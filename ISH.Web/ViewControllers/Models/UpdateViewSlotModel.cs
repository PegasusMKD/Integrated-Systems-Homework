using ISH.Service.Dtos;
using ISH.Service.Dtos.View_Slot;

namespace Integrated_Systems_Homework.ViewControllers.Models
{
    public class UpdateViewSlotModel : UpdateViewSlotDto
    {
        public string GenreName { get; set; }
        public List<string> Genres { get; set; } = new();
    }
}
