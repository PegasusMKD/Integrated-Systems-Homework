using ExcelDataReader;
using ISH.Service.Dtos.Authentication;
using ISH.Service;
using ISH.Service.Extensions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Integrated_Systems_Homework.Controllers
{
    [Route("api/users")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IUserService _userService;

        public UsersController(IUserService userService)
        {
            this._userService = userService;
        }

        [Authorize]
        [HttpGet("get-user-details")]
        public IActionResult GetUserDetails()
        {
            var user = _userService.GetUserByClaims(HttpContext.User);
            return user != null ? Ok(user) : NotFound("User not found");
        }

        [HttpPut("update-user-details")]
        public IActionResult UpdateUserDetails([FromBody] UserDto userDto)
        {
            try
            {
                _userService.UpdateUser(userDto, "", "");
                return Ok();
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpPost("import/{from?}")]
        public async Task<IActionResult> ImportUsers([FromRoute] string from, IFormFile file)
        {
            if (!User.IsInRole(UserRoles.Administrator.GetDisplay()))
                return NotFound();

            List<UserDto> users = new List<UserDto>();
            // For .net core, the next line requires the NuGet package, 
            // System.Text.Encoding.CodePages

            System.Text.Encoding.RegisterProvider(System.Text.CodePagesEncodingProvider.Instance);
            await using (var stream = file.OpenReadStream())
            {
                using (var reader = ExcelReaderFactory.CreateReader(stream))
                {
                    await _userService.ImportUsers(reader);
                }
            }

            if (from == "view")
                return RedirectToAction("Index", "User");

            return Ok();
        }
    }
}
