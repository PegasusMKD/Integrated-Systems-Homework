namespace ISH.Service.Dtos.Cart
{
    public class TicketWithCartInteractionEvent
    {
        public Guid cartId { get; set; }
        public Guid ticketId { get; set; }
    }
}
