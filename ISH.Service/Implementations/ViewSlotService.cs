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
        private readonly IMovieGenreRepository _movieGenreRepository;
        private readonly IMapper _mapper;

        public ViewSlotService(IBaseRepository<ViewSlot> baseRepository, IMapper mapper, IMovieGenreRepository movieGenreRepository)
        {
            _baseRepository = baseRepository;
            _mapper = mapper;
            _movieGenreRepository = movieGenreRepository;
        }

        public ViewSlotDto CreateViewSlot(CreateViewSlotDto viewSlot)
        {
            var mViewSlot = _mapper.Map<ViewSlot>(viewSlot);
            mViewSlot.Genre = _movieGenreRepository.GetById(viewSlot.Genre.Id);
            var eViewSlot = _baseRepository.Create(mViewSlot);
            _baseRepository.SaveChanges();
            return _mapper.Map<ViewSlotDto>(eViewSlot);
        }

        public ViewSlotDto GetById(Guid id) => 
            _mapper.Map<ViewSlotDto>(_baseRepository.GetById(id, slot => slot.Genre));

        public List<ViewSlotDto> GetAllViewSlots() => 
            _baseRepository.GetAll(slot => slot.Genre).ConvertAll(_mapper.Map<ViewSlotDto>);

        public ViewSlotDto UpdateViewSlot(UpdateViewSlotDto viewSlot)
        {
            var mViewSlot = _mapper.Map<ViewSlot>(viewSlot);
            mViewSlot.Genre = _movieGenreRepository.GetById(viewSlot.Genre.Id);
            var eViewSlot = _baseRepository.Update(mViewSlot);
            _baseRepository.SaveChanges();
            return _mapper.Map<ViewSlotDto>(eViewSlot);
        }

        public void DeleteViewSlot(Guid id) => _baseRepository.Delete(id);
    }
}
