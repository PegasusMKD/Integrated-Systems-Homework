namespace ISH.Service.Dtos.Tickets
{
    public class UpdateTicketDto
    {
        public Guid Guid { get; set; }
        public int Price { get; set; }
        public Guid ViewSlotId { get; set; }
    }
}
