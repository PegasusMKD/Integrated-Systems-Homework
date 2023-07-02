using ISH.Service.Dtos.Tickets;

namespace ISH.Service.Dtos.View_Slot
{
    public class UpdateViewSlotDto
    {
        public Guid Guid { get; set; }
        public DateTime TimeSlot { get; set; }
        public string MovieName { get; set; }
        public MovieGenreDto Genre { get; set; }
    }
}
