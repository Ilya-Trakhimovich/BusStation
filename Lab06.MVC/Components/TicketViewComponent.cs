using Lab06.BL.Services.Interfaces;
using System;

namespace Lab06.MVC.Components
{
    public class TicketViewComponent
    {
        private readonly IAppUserService _userService;

        public TicketViewComponent(IAppUserService userService)
        {
            _userService = userService;
        }

        public string Invoke(Guid userId)
        {
            return _userService.GetCountActiveUSerTickets(userId).ToString();
        }
    }
}
