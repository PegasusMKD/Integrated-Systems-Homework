using ISH.Service.Dtos.Cart;
using ISH.Service.Dtos.Orders;

namespace ISH.Service
{
    public interface IOrderService
    {
        OrderDto CreateOrder(CartDto cart);
        void NotifyUser(OrderDto order);
        void GenerateInvoice(OrderDto order); // TODO: See what return type needs to go here
        List<OrderDto> GetOrdersByUser(Guid userId);
        List<OrderDto> GetOrders();

    }
}
