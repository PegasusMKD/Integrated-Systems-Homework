using ISH.Service.Dtos.Tickets;

namespace Integrated_Systems_Homework.ViewControllers.Models
{
    public class CreateTicketModel : CreateTicketDto
    {
        public string returnUrl { get; set; }
    }
}
