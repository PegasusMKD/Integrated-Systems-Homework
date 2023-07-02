using ISH.Data;
using System.Linq.Expressions;

namespace ISH.Repository.Core
{
    public interface IBaseRepository<T> where T : BaseEntity
    {
        List<T> GetAll();
        T? GetById(Guid id);
        T Create(T entity);
        T Update(T entity);
        void Delete(Guid id);
        void SaveChanges();
    }
}
