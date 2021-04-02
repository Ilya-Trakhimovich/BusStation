using System.Collections.Generic;
using System.Linq;

namespace Lab06.DAL.Repositories.Interfaces
{
    public interface IRepository<T> where T : class
    {
        IQueryable<T> GetAll();
        T GetItemById(object id);
        void Create(T item);
        void Update(T item);
        void Delete(T item);
    }
}