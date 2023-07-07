using ISH.Service.Dtos.Tickets;

namespace Integrated_Systems_Homework.ViewControllers.Models
{
    public class FilterTicketsModel
    {
        public DateTime? fromDate { get; set; } = null;
        public DateTime? toDate { get; set; } = null;
        public List<TicketDto> Tickets { get; set; } = new List<TicketDto>();
    }
}
