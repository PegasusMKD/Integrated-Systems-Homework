﻿namespace ISH.Service.Dtos.View_Slot
{
    public class CreateViewSlotDto
    {
        public DateTime TimeSlot { get; set; }
        public string MovieName { get; set; }
        public MovieGenreDto Genre { get; set; }
    }
}
