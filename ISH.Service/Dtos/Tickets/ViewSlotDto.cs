namespace ISH.Service.Dtos.Tickets
{
    public class ViewSlotDto
    {
        public Guid Guid { get; set; }
        public DateTime TimeSlot { get; set; }
        public string MovieName { get; set; }
        public MovieGenreDto MovieGenre { get; set; }
    }
}
