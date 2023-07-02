using ISH.Service.Dtos.Authentication;
using ISH.Service;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Integrated_Systems_Homework.Controllers
{
    [Route("api/users")]
    [ApiController]
    [Authorize]
    public class UsersController : ControllerBase
    {
        private readonly IUserService _userService;

        public UsersController(IUserService userService)
        {
            this._userService = userService;
        }

        [HttpGet("get-user-details")]
        public IActionResult GetUserDetails([FromQuery(Name = "username")] string username)
        {
            var user = _userService.GetUser(username);
            if (user != null)
            {
                UserDto userDto = new()
                {
                    Id = user.Id,
                    UserName = user.UserName,
                };
                return Ok(userDto);
            }
            else
                return NotFound("User not found");
        }

        [HttpPut("update-user-details")]
        public IActionResult UpdateUserDetails([FromBody] UserDto userDTO)
        {
            try
            {
                _userService.UpdateUser(userDTO);
                return Ok();
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }
    }
}
