using ISH.Service.Dtos;
using ISH.Service.Dtos.Tickets;

namespace Integrated_Systems_Homework.ViewControllers.Models
{
    public class ExportTicketsModel
    {
        public string? SelectedGenre { get; set; } = null;
        public List<MovieGenreDto> Genres { get; set; }
        public List<TicketDto> Tickets { get; set; }
    }
}
