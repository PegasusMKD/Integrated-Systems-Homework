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
                _userService.UpdateUser(userDto);
                return Ok();
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }
    }
}
