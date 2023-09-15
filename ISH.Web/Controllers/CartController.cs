using ISH.Service;
using ISH.Service.Dtos.Cart;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Integrated_Systems_Homework.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/cart")]
    public class CartController : ControllerBase
    {
        private readonly ILogger<CartController> _logger;
        private readonly ICartService _cartService;
        private readonly IUserService _userService;


        public CartController(ILogger<CartController> logger, ICartService cartService, IUserService userService)
        {
            _logger = logger;
            _cartService = cartService;
            _userService = userService;
        }

        [HttpGet("by-user")]
        public IActionResult GetByUser()
        {
            var user = _userService.GetUserByClaims(HttpContext.User);
            return Ok(_cartService.GetCartByUser(user!.Id!));
        }

        [HttpPost("add-ticket/{ticketId}")]
        public IActionResult AddTicket([FromRoute] Guid ticketId)
        {
            var user = _userService.GetUserByClaims(HttpContext.User);
            return Ok(_cartService.AddTicket(user!.Id!, ticketId));
        }

        [HttpPost("remove-ticket/{ticketId}")]
        public IActionResult RemoveTicket([FromRoute] Guid ticketId)
        {
            var user = _userService.GetUserByClaims(HttpContext.User);
            return Ok(_cartService.RemoveTicket(user!.Id!, ticketId));
        }

        [HttpPost]
        public IActionResult CreateCart()
        {
            var user = _userService.GetUserByClaims(HttpContext.User);
            return Ok(_cartService.CreateCartForUser(user!.Id!));
        }

        [HttpDelete]
        public void DeleteCart()
        {
            var user = _userService.GetUserByClaims(HttpContext.User);
            _cartService.DeleteCartByUser(user!.Id!);
        }

        [Route("test")]
        [HttpPost]
        public void test() {}
    }
}
