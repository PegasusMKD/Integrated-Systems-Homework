using Integrated_Systems_Homework.Controllers.Extensions;
using ISH.Service;
using ISH.Service.Dtos.Orders;
using ISH.Service.Dtos.Stripe;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ViewEngines;

namespace Integrated_Systems_Homework.Controllers
{
    [Controller]
    [Route("api/order")]
    public class OrderController : Controller
    {
        private readonly ILogger<OrderController> _logger;
        private readonly IWebHostEnvironment _env;
        private readonly IOrderService _orderService;
        private readonly IUserService _userService;

        public OrderController(ILogger<OrderController> logger, IOrderService orderService, IUserService userService, IWebHostEnvironment env)
        {
            _logger = logger;
            _orderService = orderService;
            _userService = userService;
            _env = env;
        }

        [HttpPost]
        public IActionResult CreateByCart([FromBody] AddStripeCard paymentDetails)
        {
            var user = _userService.GetUserByClaims(HttpContext.User);
            return Ok(_orderService.CreateOrder(user!.Id!, paymentDetails, HttpContext.RequestAborted));
        }

        [HttpGet("invoice/{id}")]
        public IActionResult GenerateInvoice([FromRoute] Guid id)
        {
            var data = _orderService.GetOrderById(id);
            IronPdf.Installation.TempFolderPath = $@"{_env.ContentRootPath}/irontemp/";
            IronPdf.Installation.LinuxAndDockerDependenciesAutoConfig = true;
            var html = this.RenderViewAsync("_InvoicePdf", data);
            var ironPdfRender = new IronPdf.ChromePdfRenderer();
            using var pdfDoc = ironPdfRender.RenderHtmlAsPdf(html.Result);
            return File(pdfDoc.Stream.ToArray(), "application/pdf", "invoice.pdf");
        }

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
