using AutoMapper;
using ISH.Repository;
using ISH.Repository.Core;
using ClosedXML.Excel;
using ISH.Data.Tickets;
using ISH.Service.Dtos.Tickets;
using ISH.Service.Extensions;

namespace ISH.Service.Implementations
{
    public class TicketService : ITicketService
    {
        private readonly IBaseRepository<Ticket> _baseRepository;
        private readonly IMapper _mapper;
        private readonly ITicketRepository _ticketRepository;
        private readonly IBaseRepository<ViewSlot> _baseViewSlotRepository;

        public TicketService(IBaseRepository<Ticket> ticketRepository, IMapper mapper, ITicketRepository ticketRepository1, IBaseRepository<ViewSlot> baseViewSlotRepository)
        {
            _baseRepository = ticketRepository;
            _mapper = mapper;
            _ticketRepository = ticketRepository1;
            _baseViewSlotRepository = baseViewSlotRepository;
        }

        public TicketDto CreateTicket(CreateTicketDto ticket)
        {
            var mTicket = _mapper.Map<Ticket>(ticket);
            mTicket.ViewSlot = _baseViewSlotRepository.GetById(ticket.ViewSlotId)!;
            mTicket.SeatNumber = _ticketRepository.CountTicketsByViewSlot(ticket.ViewSlotId) + 1;
            var eTicket = _baseRepository.Create(mTicket);
            _baseRepository.SaveChanges();
            return _mapper.Map<TicketDto>(eTicket);
        }

        public TicketDto GetById(Guid id) => _mapper.Map<TicketDto>(_ticketRepository.GetTicketWithViewSlot(id));

        public List<TicketDto> CreateXTicketsForViewSlot(Guid viewSlotId, int xTickets, int price)
        {
            ViewSlot? eViewSlot = _baseViewSlotRepository.GetById(viewSlotId);
            if (eViewSlot == null)
                throw new Exception("View Slot does not exist!");

            int latestSeatNumber = _ticketRepository.CountTicketsByViewSlot(viewSlotId);
            var tickets = Enumerable.Range(0, xTickets).Select(x => new Ticket()
            {
                SeatNumber = latestSeatNumber + x,
                ViewSlot = eViewSlot,
                Price = price
            }).Select(_baseRepository.Create)
                .Select(_mapper.Map<TicketDto>)
                .ToList();
            _baseRepository.SaveChanges();
            return tickets;
        }

        public List<TicketDto> GetAllTickets() => _ticketRepository.GetAllTicketsWithViewSlot().Select(_mapper.Map<TicketDto>).ToList();

        public List<TicketDto> GetTicketsByViewSlot(Guid viewSlotId) => _ticketRepository
            .GetTicketsByViewSlot(viewSlotId).ConvertAll(_mapper.Map<TicketDto>);

        public List<TicketDto> FilterTickets(FilterTicketsDto filter) => 
            _ticketRepository.FilterByDates(filter.FromTimeSlot, filter.ToTimeSlot).Select(_mapper.Map<TicketDto>).ToList();

        public TicketDto UpdateTicket(UpdateTicketDto ticket)
        {
            var mTicket = _mapper.Map<Ticket>(ticket);
            mTicket.ViewSlot = _baseViewSlotRepository.GetById(ticket.ViewSlotId)!;
            var eTicket = _baseRepository.Update(mTicket);
            _baseRepository.SaveChanges();
            return _mapper.Map<TicketDto>(eTicket);
        }

        public void DeleteTicket(Guid id)
        {
            _baseRepository.Delete(id);
            _baseRepository.SaveChanges();
        }

        public List<TicketDto> FilterTicketsByGenre(string? genre) =>
            _ticketRepository.GetAllTicketsByGenreWithViewSlotAndBoughtBy(genre).Select(_mapper.Map<TicketDto>).ToList();

        public void GenerateExcelFromData(XLWorkbook workbook, List<TicketDto> tickets)
        {
            var worksheet = workbook.Worksheets.Add("Tickets");
            worksheet.Cell(1, 1).Value = "Ticket Id";
            worksheet.Cell(1, 2).Value = "Movie Name";
            worksheet.Cell(1, 3).Value = "Time Slot";
            worksheet.Cell(1, 4).Value = "Genre";
            worksheet.Cell(1, 5).Value = "Status";
            worksheet.Cell(1, 6).Value = "Seat Number";
            worksheet.Cell(1, 7).Value = "Price";
            worksheet.Cell(1, 8).Value = "Bought By";
            for (var idx = 1; idx < tickets.Count; idx++)
            {
                var ticket = tickets[idx];
                worksheet.Cell(idx + 1, 1).Value = ticket.Guid.ToString();
                worksheet.Cell(idx + 1, 2).Value = ticket.ViewSlot.MovieName;
                worksheet.Cell(idx + 1, 3).Value = ticket.ViewSlot.TimeSlot;
                worksheet.Cell(idx + 1, 4).Value = ticket.ViewSlot.Genre.Name;
                worksheet.Cell(idx + 1, 5).Value = ticket.TicketStatus.GetDisplay();
                worksheet.Cell(idx + 1, 6).Value = ticket.SeatNumber;
                worksheet.Cell(idx + 1, 7).Value = ticket.Price;
                worksheet.Cell(idx + 1, 8).Value = ticket.BoughtBy != null ? ticket.BoughtBy.UserName : "";
            }
        }
    }
}
