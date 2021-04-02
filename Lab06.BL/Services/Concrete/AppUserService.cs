using Lab06.BL.Services.Interfaces;
using Lab06.DAL.Entities;
using Lab06.DAL.UOW.Interfaces;
using System;
using System.Linq;

namespace Lab06.BL.Services.Concrete
{
    public class AppUserService : IAppUserService
    {
        private readonly IUnitOfWork _uow;

        public AppUserService(IUnitOfWork uow)
        {
            _uow = uow;
        }

        public int GetCountActiveUSerTickets(Guid userId)
        {
            var user = _uow.GetRepository<ApplicationUser>().GetItemById(userId.ToString());

            return user?.Tickets.Where(t => t.ApplicationUserId == userId.ToString()).Count(t => !t.IsCanceled) ?? 0;
        }
    }
}
