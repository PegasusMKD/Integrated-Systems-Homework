using ISH.Data;

namespace ISH.Repository.Core
{
    public interface IBaseRepository<T> where T : BaseEntity
    {
        List<T> GetAll();
        T? GetById(Guid id);
        T Insert(T entity);
        T Update(T entity);
        void Delete(Guid id);
        void SaveChangesAsync();
    }
}
