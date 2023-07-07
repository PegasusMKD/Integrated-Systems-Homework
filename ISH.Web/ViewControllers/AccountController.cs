using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using ISH.Service;
using ISH.Service.Dtos.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using ISH.Data.Authentication;

namespace Integrated_Systems_Homework.ViewControllers
{
    [Controller]
    [Route("views/account")]
    public class AccountController : Controller
    {
        private readonly UserManager<User> userManager;
        private readonly SignInManager<User> signInManager;
        private readonly IAuthService _authService;
        public AccountController(IAuthService authService, UserManager<User> userManager, SignInManager<User> signInManager)
        {
            _authService = authService;
            this.userManager = userManager;
            this.signInManager = signInManager;
        }

        [HttpGet("register")]
        public IActionResult Register()
        {
            RegisterDto model = new();
            return View(model);
        }

        [HttpPost("register"), AllowAnonymous]
        public async Task<IActionResult> Register(RegisterDto request)
        {
            if (ModelState.IsValid)
            {
                await _authService.Register(request);
            }
            else
            {
                ModelState.AddModelError("message", "Email already exists.");
            }

            return View(request);
        }


        [HttpGet("login")]
        [AllowAnonymous]
        public IActionResult Login()
        {
            LoginDto model = new();
            return View(model);
        }

        [HttpPost("login")]
        [AllowAnonymous]
        public async Task<IActionResult> Login(LoginDto model)
        {
            if (ModelState.IsValid)
            {
                var user = await userManager.FindByEmailAsync(model.Email);
                if (user != null && !user.EmailConfirmed)
                {
                    ModelState.AddModelError("message", "Email not confirmed yet");
                    return View(model);

                }
                if (await userManager.CheckPasswordAsync(user, model.Password) == false)
                {
                    ModelState.AddModelError("message", "Invalid credentials");
                    return View(model);

                }

                var result = await signInManager.PasswordSignInAsync(model.Email, model.Password, true, true);

                if (result.Succeeded)
                {
                    var userRoles = await userManager.GetRolesAsync(user);
                    await userManager.AddClaimsAsync(user, userRoles.Select(userRole => new Claim(ClaimTypes.Role, userRole)));
                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    ModelState.AddModelError("message", "Invalid login attempt");
                    return View(model);
                }
            }
            return View(model);
        }


        [HttpPost("logout")]
        public async Task<IActionResult> Logout()
        {
            await signInManager.SignOutAsync();
            return RedirectToAction("Login", "Account");
        }
    }
}

