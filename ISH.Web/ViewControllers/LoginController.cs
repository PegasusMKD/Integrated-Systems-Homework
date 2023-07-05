using ISH.Service;
using ISH.Service.Dtos.Authentication;
using Microsoft.AspNetCore.Mvc;

namespace Integrated_Systems_Homework.ViewControllers
{
    [Controller]
    [Route("views/login")]
    public class LoginController : Controller
    {
        private readonly IAuthService _authService;

        public LoginController(IAuthService authService)
        {
            _authService = authService;
        }

        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login([FromForm] LoginDto login)
        {
            var tokenData = await _authService.Login(login);
            return RedirectToPage("");
        }
    }
}
