using ISH.Service;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Integrated_Systems_Homework.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/order")]
    public class OrderController : ControllerBase
    {
        private readonly ILogger<OrderController> _logger;
        private readonly IOrderService _orderService;
        private readonly IUserService _userService;

        public OrderController(ILogger<OrderController> logger, IOrderService orderService, IUserService userService)
        {
            _logger = logger;
            _orderService = orderService;
            _userService = userService;
        }

        [HttpPost]
        public IActionResult CreateByCart()
        {
            var user = _userService.GetUserByClaims(HttpContext.User);
            return Ok(_orderService.CreateOrder(user!.Id!));
        }

        // TODO: Implement
        [HttpGet("{id}")]
        public void GenerateInvoice([FromRoute] Guid id) => _orderService.GenerateInvoice(id);

        [HttpGet]
        public IActionResult GetAll() => Ok(_orderService.GetOrders());

        [HttpGet("by-user")]
        public IActionResult GetByUser()
        {
            var user = _userService.GetUserByClaims(HttpContext.User);
            return Ok(_orderService.GetOrdersByUser(user!.Id!));
        }
    }
}
