using System.Security.Claims;
using Integrated_Systems_Homework.ViewControllers.Models;
using ISH.Data.Tickets;
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
        private readonly IMovieGenreService _movieGenreService;
        private readonly ILogger<ViewSlotController> _logger;

        public ViewSlotController(ILogger<ViewSlotController> logger, ITicketService ticketService, IViewSlotService viewSlotService, IMovieGenreService movieGenreService)
        {
            _logger = logger;
            _ticketService = ticketService;
            _viewSlotService = viewSlotService;
            _movieGenreService = movieGenreService;
        }

        // GET: ViewSlot
        [HttpGet]
        public IActionResult Index()
        {
            return View(this._viewSlotService.GetAllViewSlots());
        }

        // GET: ViewSlot/Details/5
        [HttpGet("details/{id}")]
        public IActionResult Details([FromRoute] Guid id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var viewSlot = this._viewSlotService.GetById(id);
            if (viewSlot == null)
            {
                return NotFound();
            }

            return View(viewSlot);
        }

        // GET: ViewSlot/Create
        [HttpGet("create")]
        public IActionResult Create()
        {
            return View(new CreateViewSlotModel
            {
                TimeSlot = DateTime.Now,
                Genres = _movieGenreService.GetAll().Select(item => item.Name).ToList()
            });
        }

        // POST: ViewSlot/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost("create")]
        [ValidateAntiForgeryToken]
        public IActionResult Create(CreateViewSlotModel viewSlot)
        {
            viewSlot.Genres = _movieGenreService.GetAll().Select(item => item.Name).ToList();
            if (ModelState.IsValid)
            {
                viewSlot.GenreId = _movieGenreService.GetByName(viewSlot.GenreName).Id;
                this._viewSlotService.CreateViewSlot(viewSlot);
                return RedirectToAction(nameof(Index));
            }
            return View(viewSlot);
        }

        // GET: ViewSlot/Edit/5
        [HttpGet("edit/{id}")]
        public IActionResult Edit([FromRoute] Guid id)
        {
            var viewSlot = this._viewSlotService.GetById(id);
            if (viewSlot == null)
            {
                return NotFound();
            }
            return View(new UpdateViewSlotModel
            {
                GenreId = viewSlot.Genre.Id,
                GenreName = viewSlot.Genre.Name,
                Genres = _movieGenreService.GetAll().Select(item => item.Name).ToList(),
                Guid = viewSlot.Guid,
                MovieName = viewSlot.MovieName,
                TimeSlot = viewSlot.TimeSlot
            });
        }

        // POST: ViewSlot/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost("edit/{id}")]
        [ValidateAntiForgeryToken]
        public IActionResult Edit([FromRoute] Guid id, UpdateViewSlotModel viewSlot)
        {
            if (id != viewSlot.Guid)
            {
                return NotFound();
            }

            viewSlot.Genres = _movieGenreService.GetAll().Select(item => item.Name).ToList();

            if (ModelState.IsValid)
            {
                try
                {
                    viewSlot.GenreId = _movieGenreService.GetByName(viewSlot.GenreName).Id;
                    this._viewSlotService.UpdateViewSlot(viewSlot);
                }
                catch (DbUpdateConcurrencyException)
                {
                }
                return RedirectToAction(nameof(Index));
            }
            return View(viewSlot);
        }

        // GET: ViewSlot/Delete/5
        [HttpGet("delete/{id}")]
        public IActionResult Delete([FromRoute] Guid id)
        {

            if (id == null)
            {
                return NotFound();
            }

            var viewSlot = this._viewSlotService.GetById(id);
            if (viewSlot == null)
            {
                return NotFound();
            }

            return View(viewSlot);
        }

        // POST: ViewSlot/Delete/5
        [HttpPost("delete/{id}"), ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed([FromRoute] Guid id)
        {
            this._viewSlotService.DeleteViewSlot(id);
            return RedirectToAction(nameof(Index));
        }

        private bool TicketExists(Guid id) => 
            this._ticketService.GetById(id) != null;
    }
}
