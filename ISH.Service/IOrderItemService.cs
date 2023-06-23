using ISH.Service.Dtos.Orders;

namespace ISH.Service
{
    public interface IOrderItemService
    {
        List<OrderItemDto> GetOrderItemsByOrderId(Guid orderId);
        List<OrderItemDto> CreateOrderItems(List<OrderItemDto> orderItems);
    }
}
