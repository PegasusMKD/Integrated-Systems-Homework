using ISH.Data.Tickets;
using ISH.Service.Dtos.Authentication;

namespace ISH.Service.Dtos.Tickets
{
    public class CreateTicketDto
    {
        public int Price { get; set; }
        public Guid ViewSlotId { get; set; }
    }
}
