using ISH.Service;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace Integrated_Systems_Homework.ViewControllers
{
    [Controller]
    [Route("views/orders")]
    public class OrdersController : Controller
    {
        private readonly IOrderService _orderService;

        public OrdersController(IOrderService orderService)
        {
            _orderService = orderService;
        }

        [HttpGet]
        public IActionResult Index()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            return View(_orderService.GetOrdersByUser(userId));
        }
    }
}
