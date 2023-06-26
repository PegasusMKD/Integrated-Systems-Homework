using ISH.Service.Dtos.Cart;
using ISH.Service.Dtos.Tickets;

namespace ISH.Service
{
    public interface ICartService
    {
        CartDto AddTicket(CartDto cart, TicketDto ticket);
        CartDto RemoveTicket(CartDto cart, TicketDto ticket);
        CartDto GetCartById(Guid id);
        void DeleteCartById(Guid id);
        CartDto GetCartByUser(Guid userId);
    }
}
