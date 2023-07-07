using System.Security.Claims;
using AutoMapper;
using Integrated_Systems_Homework.ViewControllers.Models;
using ISH.Service;
using ISH.Service.Dtos.Tickets;
using Microsoft.AspNetCore.Mvc;

namespace Integrated_Systems_Homework.ViewControllers
{
    [Controller]
    [Route("views/tickets")]
    public class TicketController : Controller
    {
        private readonly ITicketService _ticketService;
        private readonly ICartService _cartService;
        private readonly IMapper _mapper;

        public TicketController(ITicketService ticketService, ICartService cartService, IMapper mapper)
        {
            _ticketService = ticketService;
            _cartService = cartService;
            _mapper = mapper;
        }

        // Add to cart
        [HttpGet("add-to-cart/{id}")]
        public IActionResult AddToCart([FromRoute] Guid id, [Bind("returnUrl")] string returnUrl)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            _cartService.AddTicket(userId, id);
            return Redirect(returnUrl);
        }

        // Edit
        [HttpGet("edit/{id}")]
        public IActionResult Edit([FromRoute] Guid id, string returnUrl)
        {
            var model = new UpdateTicketModel();
            var ticket = _ticketService.GetById(id);
            model.Price = ticket.Price;
            model.ViewSlotId = ticket.ViewSlot.Guid;
            model.Guid = ticket.Guid;
            model.returnUrl = returnUrl;
            return View(model);
        }

        [HttpPost("edit/{id}")]
        public IActionResult Edit([Bind("Guid,ViewSlotId,Price,returnUrl")] UpdateTicketModel updateTicketModel)
        {
            _ticketService.UpdateTicket(updateTicketModel);
            return Redirect(updateTicketModel.returnUrl);
        }
        // Create
        [HttpGet("create/{viewSlotId}")]
        public IActionResult Create([FromRoute] Guid viewSlotId, string returnUrl)
        {
            return View(new CreateTicketModel
            {
                ViewSlotId = viewSlotId,
                returnUrl = returnUrl
            });
        }

        [HttpPost("create/{id}")]
        public IActionResult Create([Bind("Price,ViewSlotId,returnUrl")] CreateTicketModel createTicketDto)
        {
            _ticketService.CreateTicket(createTicketDto);
            return Redirect(createTicketDto.returnUrl);
        }

        // Add X To View Slot
        [HttpGet("create-x-tickets/{viewSlotId}")]
        public IActionResult AddXToViewSlot([FromRoute] Guid viewSlotId, string returnUrl)
        {
            return View(new CreateXTicketsModel
            {
                viewSlotId = viewSlotId,
                returnUrl = returnUrl
            });
        }

        [HttpPost("create-x-tickets/{viewSlotId}")]
        public IActionResult AddXToViewSlot([Bind("Price,xTickets,viewSlotId,returnUrl")] CreateXTicketsModel createXTicketsModel)
        {
            _ticketService.CreateXTicketsForViewSlot(createXTicketsModel.viewSlotId, createXTicketsModel.xTickets,
                 createXTicketsModel.Price);
            return Redirect(createXTicketsModel.returnUrl);
        }

        [HttpGet("back")]
        public IActionResult Back(string returnUrl)
        {
            return Redirect(returnUrl);
        }

        // Delete
        [HttpGet("delete/{id}")]
        public IActionResult Delete([FromRoute] Guid id, string returnUrl)
        {
            _ticketService.DeleteTicket(id);
            return Redirect(returnUrl);
        }

        // Index
        [HttpGet]
        public IActionResult Index(FilterTicketsModel model)
        {
            model.Tickets = _ticketService.FilterTickets(new FilterTicketsDto
            {
                FromTimeSlot = model.fromDate,
                ToTimeSlot = model.toDate
            });

            return View(model);
        }
    }
}
