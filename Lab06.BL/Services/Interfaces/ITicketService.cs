using Lab06.BL.DTO.TicketDTO;
using Lab06.DAL.Entities;
using System;
using System.Collections.Generic;

namespace Lab06.BL.Services.Interfaces
{
    public interface ITicketService
    {
        BuyTicketOpertaionDto BuyTicket(int tripId, Guid userId);
        List<TicketDto> GetUserTickets(Guid userId);
        List<TicketDto> GetAll();
        TicketDto GetItemById(int ticketId);
        void UpdateItem(TicketDto ticketdto);
        void CancelTicket(int ticketId);
    }
}
