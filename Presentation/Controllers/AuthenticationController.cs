using Microsoft.AspNetCore.Mvc;
using Presentation.ActionFilters;
using Service.Contracts;
using Shared.DTOs;

namespace Presentation.Controllers
{
    [ApiVersion("1.0")]
    [Route("api/authentication")]
    [ApiController]
    public class AuthenticationController : ControllerBase
    {
        private readonly IServiceManager _serviceManager;
        public AuthenticationController(IServiceManager service) => _serviceManager = service;

        [HttpPost]
        [ServiceFilter(typeof(ValidationFilterAttribute))]
        public async Task<IActionResult> RegisterUser([FromBody] UserCreationDto userCreationDto)
        {
            var result = await _serviceManager.AuthenticationService.RegisterUser(userCreationDto);

            if (!result.Succeeded)
            {
                foreach (var error in result.Errors)
                {
                    ModelState.TryAddModelError(error.Code, error.Description);
                }
                return BadRequest(ModelState);
            }
            return StatusCode(201);
        }

        [HttpPost("login")]
        [ServiceFilter(typeof(ValidationFilterAttribute))]
        public async Task<IActionResult> Authenticate([FromBody] UserAuthenticationDto user)
        {
            if (!await _serviceManager.AuthenticationService.ValidateUser(user))
                return Unauthorized();

            var token = 
                await _serviceManager.AuthenticationService.CreateToken(setExpiration: true);

            return Ok(token);
        }

    }
}
