using ISH.Service;
using ISH.Service.Dtos.Cart;
using Microsoft.AspNetCore.Mvc;

namespace Integrated_Systems_Homework.Controllers
{
    [ApiController]
    [Route("api/cart")]
    public class CartController : ControllerBase
    {
        private readonly ILogger<CartController> _logger;
        private readonly ICartService _cartService;


        public CartController(ILogger<CartController> logger, ICartService cartService)
        {
            _logger = logger;
            _cartService = cartService;
        }

        [HttpGet("by-user/{id}")]
        public IActionResult GetByUser([FromRoute] string id) => Ok(_cartService.GetCartByUser(id));

        [HttpGet("{id}")]
        public IActionResult GetById([FromRoute] Guid id) => Ok(_cartService.GetCartById(id));

        [HttpPost("add")]
        public IActionResult AddTicket([FromBody] TicketWithCartInteractionEvent interactionEvent) => 
            Ok(_cartService.AddTicket(interactionEvent.cartId, interactionEvent.ticketId));

        [HttpPost("remove")]
        public IActionResult RemoveTicket([FromBody] TicketWithCartInteractionEvent interactionEvent) => 
            Ok(_cartService.RemoveTicket(interactionEvent.cartId, interactionEvent.ticketId));

        [HttpDelete("{id}")]
        public void DeleteCart([FromRoute] Guid id) => _cartService.DeleteCartById(id);
    }
}
