using ISH.Service.Dtos.Tickets;

namespace ISH.Service
{
    public interface IViewSlotService
    {
        ViewSlotDto CreateViewSlot(ViewSlotDto viewSlot);
        ViewSlotDto GetById(Guid id);
        List<ViewSlotDto> GetAllViewSlots();
        ViewSlotDto UpdateViewSlot(ViewSlotDto viewSlot);
        void DeleteViewSlot(Guid id);
    }
}
