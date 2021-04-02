using Lab06.DAL.Entities;
using System;

namespace Lab06.BL.Services.Interfaces
{
    public interface IAppUserService
    {
        int GetCountActiveUSerTickets(Guid userId);
    }
}
