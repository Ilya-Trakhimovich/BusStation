using Lab06.DAL.Entities;
using Lab06.DAL.Repositories.Interfaces;

namespace Lab06.DAL.UOW.Interfaces
{
    public interface IUnitOfWork
    {
        IRepository<T> GetRepository<T>() where T : class;

        void Save();
    }
}
