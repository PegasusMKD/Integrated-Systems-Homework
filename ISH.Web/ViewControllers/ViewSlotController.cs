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
    [Controller]
    [Route("views/view-slot")]
    public class ViewSlotController : Controller
    {
        private readonly ITicketService _ticketService;
        private readonly IViewSlotService _viewSlotService;
        private readonly ILogger<ViewSlotController> _logger;

        public ViewSlotController(ILogger<ViewSlotController> logger, ITicketService productService, IViewSlotService viewSlotService)
        {
            _logger = logger;
            _ticketService = productService;
            _viewSlotService = viewSlotService;
        }

        // GET: ViewSlot
        [HttpGet]
        public IActionResult Index()
        {
            _logger.LogInformation("User Request -> Get All products!");
            return View(this._viewSlotService.GetAllViewSlots());
        }

        // GET: ViewSlot/Details/5
        [HttpGet("details/{id}")]
        public IActionResult Details([FromRoute] Guid id)
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

        // GET: ViewSlot/Create
        [HttpGet("create")]
        public IActionResult Create()
        {
            _logger.LogInformation("User Request -> Get create form for Product!");
            return View();
        }

        // POST: ViewSlot/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost("create")]
        [ValidateAntiForgeryToken]
        public IActionResult Create([Bind("MovieName,GenreId,TimeSlot")] CreateViewSlotDto viewSlot)
        {
            _logger.LogInformation("User Request -> Inser Product in DataBase!");
            if (ModelState.IsValid)
            {
                this._viewSlotService.CreateViewSlot(viewSlot);
                return RedirectToAction(nameof(Index));
            }
            return View(viewSlot);
        }

        // GET: ViewSlot/Edit/5
        [HttpGet("edit/{id}")]
        public IActionResult Edit([FromRoute] Guid id)
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

        // POST: ViewSlot/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost("edit/{id}")]
        [ValidateAntiForgeryToken]
        public IActionResult Edit([FromRoute] Guid id, [Bind("Guid,MovieName,TimeSlot,Genre.Id")] UpdateViewSlotDto product)
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

        // GET: ViewSlot/Delete/5
        [HttpGet("delete/{id}")]
        public IActionResult Delete([FromRoute] Guid id)
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

        // POST: ViewSlot/Delete/5
        [HttpPost("delete/{id}"), ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed([FromRoute] Guid id)
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
        //        return RedirectToAction("Index", "ViewSlot");
        //    }
        //    return View(model);
        //}

        private bool ProductExists(Guid id) => 
            this._ticketService.GetById(id) != null;
    }
}
