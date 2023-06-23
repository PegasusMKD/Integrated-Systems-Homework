using AutoMapper;
using ISH.Data.Tickets;
using ISH.Repository.Core;
using ISH.Service.Dtos.Tickets;

namespace ISH.Service.Implementations
{
    public class ViewSlotService : IViewSlotService
    {
        private readonly IBaseRepository<ViewSlot> _baseRepository;
        private IMapper _mapper;

        public ViewSlotService(IBaseRepository<ViewSlot> baseRepository, IMapper mapper)
        {
            _baseRepository = baseRepository;
            _mapper = mapper;
        }

        public ViewSlotDto CreateViewSlot(ViewSlotDto viewSlot) =>
            _mapper.Map<ViewSlotDto>(_baseRepository.Create(_mapper.Map<ViewSlot>(viewSlot)));

        public ViewSlotDto GetById(Guid id) => _mapper.Map<ViewSlotDto>(_baseRepository.GetById(id));

        public List<ViewSlotDto> GetAllViewSlots() =>
            _baseRepository.GetAll().Select(slot => _mapper.Map<ViewSlotDto>(slot)).ToList();

        public ViewSlotDto UpdateViewSlot(ViewSlotDto viewSlot) =>
            _mapper.Map<ViewSlotDto>(_baseRepository.Update(_mapper.Map<ViewSlot>(viewSlot)));

        public void DeleteViewSlot(Guid id) => _baseRepository.Delete(id);
    }
}
