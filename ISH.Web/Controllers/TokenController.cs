using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Integrated_Systems_Homework.Controllers
{
    [Route("api/auth")]
    [ApiController]
    [Authorize]
    public class TokenController : ControllerBase
    {
        [HttpPost]
        [Route("is-valid-token")]
        public IActionResult TokenIsValid() => Ok();

    }
}
