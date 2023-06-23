using ISH.Data;
using Microsoft.EntityFrameworkCore;

namespace ISH.Repository.Core
{
    public class BaseRepository<T>: IBaseRepository<T> where T : BaseEntity
    {
        private readonly ApplicationContext context;
        private DbSet<T> _dataset;


        public List<T> GetAll() => _dataset.ToList();

        public T? GetById(Guid id) => _dataset.SingleOrDefault(e => e.Guid == id);

        public T Create(T entity) => _dataset.Add(entity).Entity;

        public T Update(T entity) => _dataset.Update(entity).Entity;

        public void Delete(Guid id) => _dataset.Remove(GetById(id)!);

        public void SaveChangesAsync() => context.SaveChangesAsync();
    }
}
