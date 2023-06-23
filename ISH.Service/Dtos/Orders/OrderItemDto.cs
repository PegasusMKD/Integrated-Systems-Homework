namespace ISH.Service.Dtos.Orders
{
    public class OrderItemDto
    {
        public Guid Guid { get; set; }
        public int TicketPrice { get; set; }
        public int ItemPrice { get; set; }
        public int Quantity { get; set; } = 1;
        public DateTime TimeSlot { get; set; }
        public string MovieName { get; set; }
        public string? Comment { get; set; }
    }
}
