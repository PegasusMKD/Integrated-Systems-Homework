using System.Security.Claims;
using ISH.Service;
using ISH.Service.Dtos.Tickets;
using ISH.Service.Dtos.View_Slot;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Stripe;

namespace Integrated_Systems_Homework.ViewControllers
{
    public class ProductsController : Controller
    {
        private readonly ITicketService _ticketService;
        private readonly IViewSlotService _viewSlotService;
        private readonly ILogger<ProductsController> _logger;

        public ProductsController(ILogger<ProductsController> logger, ITicketService productService, IViewSlotService viewSlotService)
        {
            _logger = logger;
            _ticketService = productService;
            _viewSlotService = viewSlotService;
        }
            
        // GET: Products
        public IActionResult Index()
        {
            _logger.LogInformation("User Request -> Get All products!");
            return View(this._viewSlotService.GetAllViewSlots());
        }

        // GET: Products/Details/5
        public IActionResult Details(Guid id)
        {
            _logger.LogInformation("User Request -> Get Details For Product");
            if (id == null)
            {
                return NotFound();
            }

            var product = this._viewSlotService.GetById(id);
            if (product == null)
            {
                return NotFound();
            }

            return View(product);
        }

        // GET: Products/Create
        public IActionResult Create()
        {
            _logger.LogInformation("User Request -> Get create form for Product!");
            return View();
        }

        // POST: Products/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create([Bind("Id,ProductName,ProductImage,ProductDescription,ProductPrice,ProductRating")] CreateViewSlotDto viewSlot)
        {
            _logger.LogInformation("User Request -> Inser Product in DataBase!");
            if (ModelState.IsValid)
            {
                this._viewSlotService.CreateViewSlot(viewSlot);
                return RedirectToAction(nameof(Index));
            }
            return View(viewSlot);
        }

        // GET: Products/Edit/5
        public IActionResult Edit(Guid id)
        {
            _logger.LogInformation("User Request -> Get edit form for Product!");
            // TODO: Check how to actually implement this
            var product = this._viewSlotService.GetById(id);
            if (product == null)
            {
                return NotFound();
            }
            return View(product);
        }

        // POST: Products/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(Guid id, [Bind("Id,ProductName,ProductImage,ProductDescription,ProductPrice,ProductRating")] UpdateViewSlotDto product)
        {
            _logger.LogInformation("User Request -> Update Product in DataBase!");

            if (id != product.Guid)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    this._viewSlotService.UpdateViewSlot(product);
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ProductExists(product.Guid))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(product);
        }

        // GET: Products/Delete/5
        public IActionResult Delete(Guid id)
        {
            _logger.LogInformation("User Request -> Get delete form for Product!");

            if (id == null)
            {
                return NotFound();
            }

            var product = this._viewSlotService.GetById(id);
            if (product == null)
            {
                return NotFound();
            }

            return View(product);
        }

        // POST: Products/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(Guid id)
        {
            _logger.LogInformation("User Request -> Delete Product in DataBase!");

            this._viewSlotService.DeleteViewSlot(id);
            return RedirectToAction(nameof(Index));
        }

        // TODO: Implement "add ticket to card"
        //public IActionResult AddProductToCard(Guid id)
        //{
        //    var result = this._ticketService.GetShoppingCartInfo(id);

        //    return View(result);
        //}

        // TODO: Implement "add ticket to card"
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public IActionResult AddProductToCard(AddToShoppingCardDto model)
        //{

        //    _logger.LogInformation("User Request -> Add Product in ShoppingCart and save changes in database!");


        //    var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

        //    var result = this._ticketService.AddToShoppingCart(model, userId);

        //    if(result)
        //    {
        //        return RedirectToAction("Index", "Products");
        //    }
        //    return View(model);
        //}

        private bool ProductExists(Guid id) => 
            this._ticketService.GetById(id) != null;
    }
}
