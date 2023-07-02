using ISH.Service.Dtos.Cart;
using ISH.Service.Dtos.Orders;

namespace ISH.Service
{
    public interface IOrderService
    {
        OrderDto CreateOrder(string userId);
        void NotifyUser(OrderDto order);
        void GenerateInvoice(Guid orderId); // TODO: See what return type needs to go here
        List<OrderDto> GetOrdersByUser(string userId);
        List<OrderDto> GetOrders();

    }
}
