using ISH.Service.Dtos.Cart;
using ISH.Service.Dtos.Orders;
using ISH.Service.Dtos.Stripe;

namespace ISH.Service
{
    public interface IOrderService
    {
        OrderDto? CreateOrder(string userId, AddStripeCard paymentDetails, CancellationToken ct);
        void NotifyUser(OrderDto order);
        List<OrderDto> GetOrdersByUser(string userId);
        OrderDto GetOrderById(Guid orderId);
        List<OrderDto> GetOrders();

    }
}
