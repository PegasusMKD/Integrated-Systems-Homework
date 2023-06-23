using ISH.Service.Dtos.Tickets;

namespace ISH.Service
{
    public interface ITicketService
    {
        TicketDto CreateTicket(TicketDto ticket);
        List<TicketDto> CreateXTicketsForViewSlot(ViewSlotDto viewSlot, int xTickets);
        List<TicketDto> GetAllTickets();
        List<TicketDto> GetTicketsByViewSlot(ViewSlotDto viewSlot);
        List<TicketDto> FilterTickets(FilterTicketsDto filter);
    }
}
