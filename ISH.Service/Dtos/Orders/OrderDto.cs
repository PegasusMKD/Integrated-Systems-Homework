using ISH.Service.Dtos.Authentication;

namespace ISH.Service.Dtos.Orders
{
    public class OrderDto
    {
        public Guid Guid { get; set; }
        public string OrderName { get; set; }
        public int TotalPrice { get; set; }
        public UserDto OrderedBy { get; set; }
        public List<OrderItemDto> Items { get; set; }
    }
}
