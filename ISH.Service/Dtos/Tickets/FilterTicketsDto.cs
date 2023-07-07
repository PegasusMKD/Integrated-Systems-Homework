namespace ISH.Service.Dtos.Tickets
{
    public class FilterTicketsDto
    {
        public DateTime? FromTimeSlot { get; set; }
        public DateTime? ToTimeSlot { get; set; }
        public Guid ViewSlotId { get; set; }
        public bool IsAvailable { get; set; }
    }
}
