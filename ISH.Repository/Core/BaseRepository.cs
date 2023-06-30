using ISH.Data;
using Microsoft.EntityFrameworkCore;

namespace ISH.Repository.Core
{
    public class BaseRepository<T>: IBaseRepository<T> where T : BaseEntity
    {
        private readonly ApplicationContext context;


        public List<T> GetAll() => context.Set<T>().ToList();

        public T? GetById(Guid id) => context.Set<T>().SingleOrDefault(e => e.Guid == id);

        public T Create(T entity) => context.Set<T>().Add(entity).Entity;

        public T Update(T entity) => context.Set<T>().Update(entity).Entity;

        public void Delete(Guid id) => context.Set<T>().Remove(GetById(id)!);

        public void SaveChangesAsync() => context.SaveChangesAsync();
    }
}
