using Lab06.DAL.DbContext;
using Lab06.DAL.Repositories.Concrete;
using Lab06.DAL.Repositories.Interfaces;
using Lab06.DAL.UOW.Interfaces;
using System;
using System.Collections.Generic;

namespace Lab06.DAL.UOW.Concrete
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ApplicationContext _context;
        private Dictionary<Type, object> _repositories;

        public UnitOfWork(ApplicationContext context)
        {
            _context = context;
        }

        public void Save()
        {
            _context.SaveChanges();
        }

        public IRepository<T> GetRepository<T>() where T : class
        {
            if (_repositories == null)
            {
                _repositories = new Dictionary<Type, object>();
            }

            var type = typeof(T);

            if (!_repositories.ContainsKey(type))
            {
                _repositories[type] = new BaseRepository<T>(_context);
            }

            return (IRepository<T>)_repositories[type];
        }
    }
}