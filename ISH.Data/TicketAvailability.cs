namespace ISH.Data
{
    public class TicketAvailability
    {
        public Guid Guid { get; set; }
        public DateTime TimeSlot { get; set; }
        public string MovieName { get; set; }
        public int AvailableTickets { get; set; } = 0;


    }
}
