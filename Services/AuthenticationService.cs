using AutoMapper;
using Contracts;
using Entities.Exceptions;
using Entities.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using Service.Contracts;
using Shared.DTOs;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Services
{
    public class AuthenticationService : IAuthenticationService
    {
        private readonly ILoggerManager _logger;
        private readonly IMapper _mapper;
        private readonly UserManager<User> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly IConfiguration _configuration;
        private User? _user;

        public AuthenticationService(
            ILoggerManager logger,
            IMapper mapper,
            UserManager<User> userManager,
            RoleManager<IdentityRole> roleManager,
            IConfiguration configuration)
        {
            _logger = logger;
            _mapper = mapper;
            _userManager = userManager;
            _roleManager = roleManager;
            _configuration = configuration;
        }

        public async Task<string> CreateToken()
        {
            var signingCredentials = GetSigningCredentials();
            var claims = await GetClaims();
            var tokenOptions = GenerateTokenOptions(signingCredentials, claims);
            return new JwtSecurityTokenHandler().WriteToken(tokenOptions);
        }

        private SigningCredentials GetSigningCredentials()
        {
            var key = Encoding.UTF8.GetBytes(Environment.GetEnvironmentVariable("SECRETKEYJWT"));
            var secret = new SymmetricSecurityKey(key);
            return new SigningCredentials(secret, SecurityAlgorithms.HmacSha256);
        }

        private async Task<List<Claim>> GetClaims()
        {
            var claims = new List<Claim>
             {
                new Claim(ClaimTypes.Name, _user.UserName)
             };
            var roles = await _userManager.GetRolesAsync(_user);
            foreach (var role in roles)
            {
                claims.Add(new Claim(ClaimTypes.Role, role));
            }
            return claims;
        }

        private JwtSecurityToken GenerateTokenOptions(SigningCredentials signingCredentials, List<Claim> claims)
        {
            var jwtSettings = _configuration.GetSection("JwtSettings");

            if (jwtSettings.Value is null)
                _logger.LogError($"{nameof(GenerateTokenOptions)}: Generation of token has failed. Empty jwtSettings.");

            var tokenOptions = new JwtSecurityToken
            (
                issuer: jwtSettings["validIssuer"],
                audience: jwtSettings["validAudience"],
                claims: claims,
                expires: DateTime.Now.AddMinutes(Convert.ToDouble(jwtSettings["expires"])),
                signingCredentials: signingCredentials
            );
            return tokenOptions;
        }

        public async Task<IdentityResult> RegisterUser(UserCreationDto userCreationDto)
        {
            var user = _mapper.Map<User>(userCreationDto);

            await ValidateRoles(userCreationDto, user);

            var result = await _userManager.CreateAsync(user, userCreationDto.Password);
            if (result.Succeeded)
            {
                await _userManager.AddToRolesAsync(user, userCreationDto.Roles);
            }

            return result;
        }

        public async Task<bool> ValidateUser(UserAuthenticationDto userAuth)
        {
            _user = await _userManager.FindByNameAsync(userAuth.UserName);
            var result = _user != null && await _userManager.CheckPasswordAsync(_user, userAuth.Password);
            if (!result)
                _logger.LogWarn($"{nameof(ValidateUser)}: Authentication failed. Wrong username or password.");
            return result;
        }

        private async Task ValidateRoles(UserCreationDto userCreationDto, User user)
        {
            var notFoundedRoles = new List<string>();

            foreach (var role in userCreationDto.Roles)
            {
                if (await _roleManager.RoleExistsAsync(role))
                {
                    continue;
                }
                notFoundedRoles.Add(role);
            }

            if (notFoundedRoles.Any())
                throw new RolesNotFoundException(notFoundedRoles);
        }
    }
}
