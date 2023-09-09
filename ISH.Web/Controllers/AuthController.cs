using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using AutoMapper;
using ISH.Data.Authentication;
using ISH.Service;
using ISH.Service.Dtos.Authentication;
using Microsoft.AspNetCore.Authentication;
using Microsoft.OpenApi.Extensions;

namespace Integrated_Systems_Homework.Controllers
{
    [Route("api/auth")]
    [ApiController]
    [AllowAnonymous]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;

        public AuthController(IAuthService authService)
        {
            _authService = authService;
        }

        [HttpPost]
        [Route("login")]
        public async Task<IActionResult> Login([FromBody] LoginDto loginDto) => Ok(await _authService.Login(loginDto));

        [HttpPost]
        [Route("logout")]
        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            var user = HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (user != null)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new());
            }
            return Ok();
        }

        [HttpPost]
        [Route("register")]
        public async Task<IActionResult> Register([FromBody] RegisterDto registerDto) =>
            Ok(await _authService.Register(registerDto));
    }
}
