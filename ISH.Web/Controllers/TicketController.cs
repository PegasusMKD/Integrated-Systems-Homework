using ISH.Service;
using ISH.Service.Dtos.Tickets;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Integrated_Systems_Homework.Controllers
{
    [ApiController]
    [AllowAnonymous]
    [Route("api/ticket")]
    public class TicketController : ControllerBase
    {
        private readonly ILogger<TicketController> _logger;
        private readonly ITicketService _ticketService;

        public TicketController(ILogger<TicketController> logger, ITicketService ticketService)
        {
            _logger = logger;
            _ticketService = ticketService;
        }

        [HttpGet("{id}")]
        public IActionResult GetById([FromRoute] Guid id) => Ok(_ticketService.GetById(id));

        [HttpGet("by-view-slot/{id}")]
        public IActionResult GetByViewSlot([FromRoute] Guid id) => Ok(_ticketService.GetTicketsByViewSlot(id));

        [HttpPost("filter")]
        public IActionResult Filter([FromBody] FilterTicketsDto filter) => Ok(_ticketService.FilterTickets(filter));

        [HttpPut]
        [Authorize]
        public IActionResult Update([FromBody] TicketDto ticket) => Ok(_ticketService.UpdateTicket(ticket));

        [HttpPost]
        [Authorize]
        public IActionResult Create([FromBody] TicketDto ticket) => Ok(_ticketService.CreateTicket(ticket));

        [Authorize]
        [HttpDelete("{id}")]
        public void Delete([FromRoute] Guid id) => _ticketService.DeleteTicket(id);

        [Authorize]
        [HttpPost("for-view-slot")]
        public IActionResult CreateXForViewSlot([FromBody] CreateXTicketsDto createXTickets) =>
            Ok(_ticketService.CreateXTicketsForViewSlot(createXTickets.viewSlotId, createXTickets.xTickets));

        [HttpGet]
        public IActionResult GetAll() => Ok(_ticketService.GetAllTickets());
    }
}