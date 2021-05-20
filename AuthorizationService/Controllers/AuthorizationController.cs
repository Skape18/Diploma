using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using AuthorizationService.Models;
using AuthorizationService.Services;
using Microsoft.Extensions.Configuration;

namespace AuthorizationService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthorizationController : ControllerBase
    {
        private readonly IUserService _userService;
        private readonly IConfiguration _configuration;

        public AuthorizationController(IUserService userService, IConfiguration configuration)
        {
            _userService = userService;
            _configuration = configuration;
        }


        [HttpPost("login")]
        public async Task<ActionResult<string>> Login([FromBody] LoginModel loginViewModel)
        {
            var token = await _userService.SignInAsync(loginViewModel, _configuration["Tokens:Key"],
                int.Parse(_configuration["Tokens:ExpiryMinutes"]),
                _configuration["Tokens:Audience"], _configuration["Tokens:Issuer"]
            );

            return Ok(token);
        }

        [HttpPost("registration")]
        public async Task<ActionResult<string>> Register([FromBody] RegistrationModel registrationViewModel)
        {
            var token = await _userService.SignUpAsync(registrationViewModel, _configuration["Tokens:Key"],
                int.Parse(_configuration["Tokens:ExpiryMinutes"]),
                _configuration["Tokens:Audience"], _configuration["Tokens:Issuer"]
            );

            return Ok(token);
        }

        [HttpGet("logout")]
        public async Task<IActionResult> Logout()
        {
            await _userService.SignOutAsync();

            return Ok();
        }
    }
}
