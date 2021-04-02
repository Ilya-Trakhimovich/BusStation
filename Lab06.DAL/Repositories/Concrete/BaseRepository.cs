using Lab06.DAL.DbContext;
using Lab06.DAL.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace Lab06.DAL.Repositories.Concrete
{
    public class BaseRepository<T> : IRepository<T> where T : class
    {
        private readonly ApplicationContext _context;
        private readonly DbSet<T> _dbSet;

        public BaseRepository(ApplicationContext context)
        {
            _context = context;
            _dbSet = context.Set<T>();
        }

        public void Create(T item)
        {
            _dbSet.Add(item);
        }

        public void Delete(T item)
        {
            _dbSet.Remove(item);
        }

        public IQueryable<T> GetAll()
        {
            return _dbSet;
        }

        public T GetItemById(object id)
        {
            return _dbSet.Find(id);
        }

        public void Update(T item)
        {
            _dbSet.Update(item);
        }
    }
}
