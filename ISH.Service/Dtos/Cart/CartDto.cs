using System.ComponentModel.DataAnnotations;
using ISH.Service.Dtos.Authentication;
using ISH.Service.Dtos.Tickets;

namespace ISH.Service.Dtos.Cart
{
    public class CartDto
    {
        public Guid Guid { get; set; }
        public UserDto User { get; set; }
        public int CartPrice { get; set; }

        public List<TicketDto> Tickets { get; } = new();
    }
}
