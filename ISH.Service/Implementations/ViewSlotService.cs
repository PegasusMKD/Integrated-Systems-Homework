using AutoMapper;
using ISH.Data.Tickets;
using ISH.Repository;
using ISH.Repository.Core;
using ISH.Service.Dtos.Tickets;
using ISH.Service.Dtos.View_Slot;

namespace ISH.Service.Implementations
{
    public class ViewSlotService : IViewSlotService
    {
        private readonly IBaseRepository<ViewSlot> _baseRepository;
        private readonly ITicketService _ticketService;
        private readonly IViewSlotRepository _viewSlotRepository;
        private readonly IMovieGenreRepository _movieGenreRepository;
        private readonly IMapper _mapper;

        public ViewSlotService(IBaseRepository<ViewSlot> baseRepository, IMapper mapper, IMovieGenreRepository movieGenreRepository, IViewSlotRepository viewSlotRepository, ITicketService ticketService)
        {
            _baseRepository = baseRepository;
            _mapper = mapper;
            _movieGenreRepository = movieGenreRepository;
            _viewSlotRepository = viewSlotRepository;
            _ticketService = ticketService;
        }

        public ViewSlotDto CreateViewSlot(CreateViewSlotDto viewSlot)
        {
            var mViewSlot = _mapper.Map<ViewSlot>(viewSlot);
            mViewSlot.Genre = _movieGenreRepository.GetById(viewSlot.GenreId);
            var eViewSlot = _baseRepository.Create(mViewSlot);
            _baseRepository.SaveChanges();
            return _mapper.Map<ViewSlotDto>(eViewSlot);
        }

        public ViewSlotDto GetById(Guid id)
        {
            var slot = _mapper.Map<ViewSlotDto>(_viewSlotRepository.GetByIdWithGenre(id));
            slot.Tickets = _ticketService.GetTicketsByViewSlot(slot.Guid);
            return slot;
        }

        public List<ViewSlotDto> GetAllViewSlots() => 
            _viewSlotRepository.GetAllWithGenre().ConvertAll(_mapper.Map<ViewSlotDto>);

        public ViewSlotDto UpdateViewSlot(UpdateViewSlotDto viewSlot)
        {
            var mViewSlot = _mapper.Map<ViewSlot>(viewSlot);
            mViewSlot.Genre = _movieGenreRepository.GetById(viewSlot.GenreId);
            var eViewSlot = _baseRepository.Update(mViewSlot);
            _baseRepository.SaveChanges();
            return _mapper.Map<ViewSlotDto>(eViewSlot);
        }

        public void DeleteViewSlot(Guid id)
        {
            _baseRepository.Delete(id);
            _baseRepository.SaveChanges();
        }
    }
}
