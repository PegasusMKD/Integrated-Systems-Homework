using ISH.Service.Dtos.Cart;
using ISH.Service.Dtos.Tickets;

namespace ISH.Service
{
    public interface ICartService
    {
        CartDto AddTicket(string userId, Guid ticketId);
        CartDto RemoveTicket(string userId, Guid ticketId);
        CartDto GetCartById(Guid id);
        void DeleteCartById(Guid id);
        void DeleteCartByUser(string id);
        CartDto CreateCartForUser(string userId);
        CartDto GetCartByUser(string userId);
    }
}
