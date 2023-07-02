using ISH.Data.Tickets;
using ISH.Service.Dtos.Authentication;

namespace ISH.Service.Dtos.Tickets
{
    public class TicketDto
    {
        public Guid Guid { get; set; }
        public int Price { get; set; }
        public string SeatNumber { get; set; }
        public ViewSlotDto ViewSlot { get; set; }
        public UserDto? BoughtBy { get; set; }
        public TicketStatus TicketStatus { get; set; }
    }
}
