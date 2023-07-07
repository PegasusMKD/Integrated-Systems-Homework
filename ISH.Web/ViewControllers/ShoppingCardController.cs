using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Stripe;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using ISH.Service;
using ISH.Service.Dtos.Cart;
using ISH.Service.Dtos.Stripe;

namespace Integrated_Systems_Homework.ViewControllers
{
    [Controller]
    [Route("views/shopping-cart")]
    public class ShoppingCardController : Controller
    {

        private readonly ICartService _shoppingCartService;
        private readonly IOrderService _orderService;

        public ShoppingCardController(ICartService shoppingCartService, IOrderService orderService)
        {
            _shoppingCartService = shoppingCartService;
            _orderService = orderService;
        }

        [HttpGet]
        public IActionResult Index()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            return View(this._shoppingCartService.GetCartByUser(userId!) ?? new CartDto());
        }

        [HttpGet("remove/{id}")]
        public IActionResult DeleteFromShoppingCart([FromRoute] Guid id)
        {

            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            var result = this._shoppingCartService.RemoveTicket(userId!, id);
            
            return RedirectToAction("Index", "ShoppingCard");
        }

        [HttpPost("pay")]
        public IActionResult PayOrder(string stripeEmail, string stripeToken, CancellationToken token)
        {
            string userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            this._orderService.CreateOrder(userId, stripeEmail, stripeToken, token);
            return RedirectToAction("Index", "ShoppingCard");
        }
    }
}
