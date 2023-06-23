using ISH.Service.Dtos.Cart;
using ISH.Service.Dtos.Tickets;

namespace ISH.Service
{
    public interface ICartService
    {
        CartDto AddTicket(TicketDto ticket);
        CartDto RemoveTicket(TicketDto ticket);
        CartDto GetCartById(Guid id);
        void DeleteCartById(Guid id);
        CartDto GetCartByUser(Guid userId);
    }
}
