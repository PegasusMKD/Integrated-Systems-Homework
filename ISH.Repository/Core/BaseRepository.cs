using ISH.Data;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore.Query;

namespace ISH.Repository.Core
{
    public class BaseRepository<T> : IBaseRepository<T> where T : BaseEntity
    {
        private readonly ApplicationContext _context;


        public BaseRepository(ApplicationContext context)
        {
            this._context = context;
        }

        public List<T> GetAll() => _context.Set<T>().ToList();

        public T? GetById(Guid id) => _context.Set<T>().SingleOrDefault(e => e.Guid == id);

        public T Create(T entity) => _context.Set<T>().Add(entity).Entity;

        public T Update(T entity) => _context.Set<T>().Update(entity).Entity;

        public void Delete(Guid id) => _context.Set<T>().Remove(GetById(id)!);

        public void SaveChanges() => _context.SaveChanges();
    }
}
