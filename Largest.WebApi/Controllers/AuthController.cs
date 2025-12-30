using Largest.Application.DTOs;
using Largest.Application.Interfaces.Services;
using Largest.Application.Services;
using Microsoft.AspNetCore.Mvc;

namespace Largest.WebApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;

        public AuthController(IAuthService authService)
        {
            _authService = authService;
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] UserLoginDto request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(new { message = "Invalid login request" });
            }
            var token = await _authService.LoginAsync(request.Email, request.Password);
            if (string.IsNullOrEmpty(token))
            {
                return Unauthorized(new {message = "Invalid email or password"});
            }
            return Ok(new { Token = token });
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] UserRegisterDto request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(new { message = "Invalid registration request" });
            }
            var result = await _authService.RegisterAsync(request);
            if (!result.Success)
            {
                return BadRequest(new { message = result.Message });
            }

            return Ok(result);
        }
    }
}
