using AutoMapper;
using Lab06.BL.DTO.TicketDTO;
using Lab06.BL.ResultEnum;
using Lab06.BL.Services.Interfaces;
using Lab06.DAL.Entities;
using Lab06.DAL.UOW.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Lab06.BL.Services.Concrete
{
    public class TicketService : ITicketService
    {
        private readonly IUnitOfWork _uow;
        private readonly IMapper _mapper;

        public TicketService(IUnitOfWork uow, IMapper mapper)
        {
            _uow = uow;
            _mapper = mapper;
        }

        public BuyTicketOpertaionDto BuyTicket(int tripId, Guid userId)
        {
            var trip = _uow.GetRepository<Trip>().GetItemById(tripId);
            var user = _uow.GetRepository<ApplicationUser>().GetItemById(userId.ToString());

            if (trip != null && user != null)
            {
                var ticket = new Ticket
                {
                    ApplicationUserId = userId.ToString(),
                    TripId = tripId
                };

                trip.FreeSeats--;
                trip.CountSoldSeats++;

                if (trip.FreeSeats >= 0 && trip.CountSoldSeats <= trip.Bus.SeatsCount)
                {
                    _uow.GetRepository<Ticket>().Create(ticket);
                    _uow.GetRepository<Trip>().Update(trip);
                    _uow.Save();

                    return new BuyTicketOpertaionDto { Result = Result.Success };
                }
                else
                {
                    return new BuyTicketOpertaionDto { Result = Result.Fail };
                }
            }
            else
            {
                return new BuyTicketOpertaionDto { Result = Result.Fail };
            }
        }

        public List<TicketDto> GetUserTickets(Guid userId)
        {
            var tickets = _uow.GetRepository<Ticket>()
                       .GetAll()
                       .Where(t => t.ApplicationUserId == userId.ToString())
                       .OrderBy(t => t.Trip.StartDate)
                       .ToList();

            var ticketsDto = _mapper.Map<List<TicketDto>>(tickets);

            return ticketsDto;
        }

        public List<TicketDto> GetAll()
        {
            var tickets = _uow.GetRepository<Ticket>().GetAll().ToList();
            var ticketsDto = _mapper.Map<List<TicketDto>>(tickets);

            return ticketsDto;
        }

        public TicketDto GetItemById(int ticketId)
        {
            var ticket = _uow.GetRepository<Ticket>().GetItemById(ticketId) ?? new Ticket();
            var ticketDto = _mapper.Map<TicketDto>(ticket);

            return ticketDto;
        }

        public void UpdateItem(TicketDto ticketdto)
        {
            var ticketRepo = _uow.GetRepository<Ticket>();
            var ticket = _mapper.Map<Ticket>(ticketdto);

            ticketRepo.Update(ticket);
            _uow.Save();
        }

        public void CancelTicket(int ticketId)
        {
            var ticketRepo = _uow.GetRepository<Ticket>();
            var ticket = ticketRepo.GetItemById(ticketId);

            ticket.IsCanceled = true;
            ticket.Trip.FreeSeats++;

            ticketRepo.Update(ticket);
            _uow.Save();
        }        
    }
}