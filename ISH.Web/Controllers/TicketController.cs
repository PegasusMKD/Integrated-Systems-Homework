using ClosedXML.Excel;
using ISH.Service;
using ISH.Service.Dtos.Tickets;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.OpenApi.Extensions;

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

        [HttpGet("by-genre")]
        public IActionResult Filter([FromQuery] string? genre) => Ok(_ticketService.FilterTicketsByGenre(genre));

        // [Authorize(Roles = "Administrator")]
        [HttpGet("excel")]
        public IActionResult GenerateExcel([FromQuery] string? genre)
        {
            const string contentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
            const string fileName = "tickets.xlsx";

            var tickets = _ticketService.FilterTicketsByGenre(genre);
            using var workbook = new XLWorkbook();
            _ticketService.GenerateExcelFromData(workbook, tickets);
            using var stream = new MemoryStream();
            workbook.SaveAs(stream);
            var content = stream.ToArray();
            return File(content, contentType, fileName);
        }

        [HttpPut]
        [Authorize]
        public IActionResult Update([FromBody] UpdateTicketDto ticket) => Ok(_ticketService.UpdateTicket(ticket));

        [HttpPost]
        [Authorize]
        public IActionResult Create([FromBody] CreateTicketDto ticket) => Ok(_ticketService.CreateTicket(ticket));

        [Authorize]
        [HttpDelete("{id}")]
        public void Delete([FromRoute] Guid id) => _ticketService.DeleteTicket(id);

        [Authorize]
        [HttpPost("for-view-slot")]
        public IActionResult CreateXForViewSlot([FromBody] CreateXTicketsDto createXTickets) =>
            Ok(_ticketService.CreateXTicketsForViewSlot(createXTickets.viewSlotId, createXTickets.xTickets, createXTickets.Price));

        [HttpGet]
        public IActionResult GetAll() => Ok(_ticketService.GetAllTickets());
    }
}