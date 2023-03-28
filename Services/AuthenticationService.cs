using AutoMapper;
using Contracts;
using Entities.Exceptions;
using Entities.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Service.Contracts;
using Shared.DTOs;

namespace Services
{
    public class AuthenticationService : IAuthenticationService
    {
        private readonly ILoggerManager _logger;
        private readonly IMapper _mapper;
        private readonly UserManager<User> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly IConfiguration _configuration;

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
