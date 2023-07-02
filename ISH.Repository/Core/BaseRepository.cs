using ISH.Data;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace ISH.Repository.Core
{
    public class BaseRepository<T> : IBaseRepository<T> where T : BaseEntity
    {
        private readonly ApplicationContext _context;


        public BaseRepository(ApplicationContext context)
        {
            this._context = context;
        }

        public List<T> GetAll<TProperty>(Expression<Func<T, TProperty>> navigationPropertyPath) => _context.Set<T>().Include(navigationPropertyPath).ToList();
        public List<T> GetAll() => _context.Set<T>().ToList();

        public T? GetById(Guid id) => _context.Set<T>().SingleOrDefault(e => e.Guid == id);

        public T? GetById<TProperty>(Guid id, Expression<Func<T, TProperty>> navigationPropertyPath) => 
            _context.Set<T>().Include(navigationPropertyPath).SingleOrDefault(e => e.Guid == id);

        public T Create(T entity) => _context.Set<T>().Add(entity).Entity;

        public T Update(T entity) => _context.Set<T>().Update(entity).Entity;

        public void Delete(Guid id) => _context.Set<T>().Remove(GetById(id)!);

        public void SaveChanges() => _context.SaveChanges();
    }
}
