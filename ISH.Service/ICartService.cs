using ISH.Service.Dtos.Cart;
using ISH.Service.Dtos.Tickets;

namespace ISH.Service
{
    public interface ICartService
    {
        CartDto AddTicket(Guid cartId, Guid ticketId);
        CartDto RemoveTicket(Guid cartId, Guid ticketId);
        CartDto GetCartById(Guid id);
        void DeleteCartById(Guid id);
        CartDto GetCartByUser(Guid userId);
    }
}
