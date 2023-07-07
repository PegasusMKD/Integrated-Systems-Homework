using Integrated_Systems_Homework.ViewControllers.Models;
using ISH.Service;
using ISH.Service.Dtos.Authentication;
using Microsoft.AspNetCore.Mvc;

namespace Integrated_Systems_Homework.ViewControllers
{
    [Controller]
    [Route("views/users")]
    public class UserController : Controller
    {
        private readonly IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpGet]
        public IActionResult Index()
        {
            return View(_userService.GetUsers());
        }

        [HttpGet("import")]
        public IActionResult Import()
        {
            return View();
        }

        [HttpGet("edit/{id}")]
        public IActionResult Edit([FromRoute] string id)
        {
            var user = _userService.GetUserById(id);
            return View(new UpdateUserModel
            {
                Id = id,
                EmailConfirmed = user.EmailConfirmed,
                Role = user.Roles.FirstOrDefault("User"),
                Email = user.Email,
                UserName = user.UserName
            });
        }

        [HttpPost("edit/{id}")]
        public IActionResult Edit([FromRoute] string Id, UpdateUserModel updateUserModel)
        {
            var user = new UserDto
            {
                Id = Id,
                EmailConfirmed = updateUserModel.EmailConfirmed,
                UserName = updateUserModel.UserName,
                Email = updateUserModel.Email,
                Roles = new string[] { updateUserModel.Role }
            };
            _userService.UpdateUser(user, updateUserModel.CurrentPassword, updateUserModel.NewPassword);
            return RedirectToAction("Index");
        }

        [HttpGet("delete/{id}")]
        public IActionResult Delete([FromRoute] string id)
        {
            _userService.DeleteUser(id);
            return RedirectToAction("Index");
        }
    }
}
