namespace ISH.Data.Tickets
{
    public class ViewSlot
    {
        /**
         * ViewSlot is just an aggregate for all available time slots in the cinema for the users to watch a movie.
         */
        public Guid Guid { get; set; }
        public DateTime TimeSlot { get; set; }
        public string MovieName { get; set; }
        public MovieGenre Genre { get; set; }

    }
}
