using ISH.Data.Tickets;

namespace ISH.Repository
{
    public interface IViewSlotRepository
    {
        ViewSlot GetByIdWithGenre(Guid id);
        List<ViewSlot> GetAllWithGenre();
    }
}
