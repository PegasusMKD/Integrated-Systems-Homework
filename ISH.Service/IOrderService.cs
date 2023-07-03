using ISH.Service.Dtos.Cart;
using ISH.Service.Dtos.Orders;
using ISH.Service.Dtos.Stripe;

namespace ISH.Service
{
    public interface IOrderService
    {
        OrderDto? CreateOrder(string userId, AddStripeCard paymentDetails, CancellationToken ct);
        void NotifyUser(OrderDto order);
        void GenerateInvoice(Guid orderId); // TODO: See what return type needs to go here
        List<OrderDto> GetOrdersByUser(string userId);
        List<OrderDto> GetOrders();

    }
}
