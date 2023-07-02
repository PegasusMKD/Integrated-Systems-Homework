using ISH.Service;
using Microsoft.AspNetCore.Mvc;

namespace Integrated_Systems_Homework.Controllers
{
    [ApiController]
    [Route("api/order")]
    public class OrderController : ControllerBase
    {
        private readonly ILogger<OrderController> _logger;
        private readonly IOrderService _orderService;

        public OrderController(ILogger<OrderController> logger, IOrderService orderService)
        {
            _logger = logger;
            _orderService = orderService;
        }

        // TODO: Update when we implement Identity
        [HttpPost("{id}")]
        public IActionResult CreateByCart([FromRoute] Guid id) => Ok(_orderService.CreateOrder(id));

        // TODO: Implement
        [HttpGet("{id}")]
        public void GenerateInvoice([FromRoute] Guid id) => _orderService.GenerateInvoice(id);

        [HttpGet]
        public IActionResult GetAll() => Ok(_orderService.GetOrders());

        [HttpGet("user/{id}")]
        public IActionResult GetByUser([FromRoute] string id) => Ok(_orderService.GetOrdersByUser(id));
    }
}
