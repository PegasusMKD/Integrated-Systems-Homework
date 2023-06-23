using AutoMapper;
using ISH.Data.Tickets;
using ISH.Repository.Core;
using ISH.Service.Dtos.Tickets;

namespace ISH.Service.Implementations
{
    public class TicketService : ITicketService
    {
        private readonly IBaseRepository<Ticket> _ticketRepository;
        private readonly IMapper _mapper;

        public TicketService(IBaseRepository<Ticket> ticketRepository, Mapper mapper)
        {
            _ticketRepository = ticketRepository;
            _mapper = mapper;
        }

        public TicketDto CreateTicket(TicketDto ticket) =>
            _mapper.Map<TicketDto>(_ticketRepository.Create(_mapper.Map<Ticket>(ticket)));

        public TicketDto GetById(Guid id) => _mapper.Map<TicketDto>(_ticketRepository.GetById(id));

        public List<TicketDto> CreateXTicketsForViewSlot(ViewSlotDto viewSlot, int xTickets)
        {
            throw new NotImplementedException();
        }

        public List<TicketDto> GetAllTickets() => _ticketRepository.GetAll().Select(ticket => _mapper.Map<TicketDto>(ticket)).ToList();

        public List<TicketDto> GetTicketsByViewSlot(ViewSlotDto viewSlot)
        {
            throw new NotImplementedException();
        }

        public List<TicketDto> FilterTickets(FilterTicketsDto filter)
        {
            throw new NotImplementedException();
        }

        public TicketDto UpdateTicket(TicketDto ticket) =>
            _mapper.Map<TicketDto>(_ticketRepository.Update(_mapper.Map<Ticket>(ticket)));

        public void DeleteTicket(Guid id) => _ticketRepository.Delete(id);
    }
}
