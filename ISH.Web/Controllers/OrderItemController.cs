using ISH.Service;
using ISH.Service.Dtos.Orders;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Integrated_Systems_Homework.Controllers
{
    // TODO: Check if this controller even needs to exist
    [Authorize]
    [ApiController]
    [Route("api/order-items")]
    public class OrderItemController : ControllerBase
    {
        private readonly ILogger<OrderItemController> _logger;
        private readonly IOrderItemService _orderItemService;

        public OrderItemController(ILogger<OrderItemController> logger, IOrderItemService orderItemService)
        {
            _logger = logger;
            _orderItemService = orderItemService;
        }

        [HttpGet("{id}")]
        public IActionResult GetByOrderId([FromRoute] Guid id) => Ok(_orderItemService.GetOrderItemsByOrderId(id));
    }
}
