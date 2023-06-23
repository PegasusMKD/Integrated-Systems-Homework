using ISH.Data.Tickets;
using ISH.Repository.Core;
using ISH.Service.Dtos.Tickets;

namespace ISH.Service.Implementations
{
    public class TicketService : ITicketService
    {
        private readonly IBaseRepository<Ticket> _ticketRepository;

        public TicketService(IBaseRepository<Ticket> ticketRepository)
        {
            _ticketRepository = ticketRepository;
        }

        public TicketDto CreateTicket(TicketDto ticket)
        {
            throw new NotImplementedException();
        }

        public TicketDto GetById(Guid id)
        {
            throw new NotImplementedException();
        }

        public List<TicketDto> CreateXTicketsForViewSlot(ViewSlotDto viewSlot, int xTickets)
        {
            throw new NotImplementedException();
        }

        public List<TicketDto> GetAllTickets()
        {
            throw new NotImplementedException();
        }

        public List<TicketDto> GetTicketsByViewSlot(ViewSlotDto viewSlot)
        {
            throw new NotImplementedException();
        }

        public List<TicketDto> FilterTickets(FilterTicketsDto filter)
        {
            throw new NotImplementedException();
        }

        public TicketDto UpdateTicket(TicketDto ticket)
        {
            throw new NotImplementedException();
        }

        public void DeleteTicket(Guid id)
        {
            throw new NotImplementedException();
        }
    }
}
