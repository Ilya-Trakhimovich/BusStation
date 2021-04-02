using AutoMapper;
using Lab06.BL.ResultEnum;
using Lab06.BL.Services.Interfaces;
using Lab06.DAL.Entities;
using Lab06.MVC.ViewModels.PaymentCard;
using Lab06.MVC.ViewModels.TicketViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;

namespace Lab06.MVC.Controllers
{
    public class TicketController : Controller
    {
        private readonly ITicketService _ticketService;
        private readonly ITripService _tripService;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IMapper _mapper;

        public TicketController(ITicketService ticketService,
                                ITripService tripService,
                                UserManager<ApplicationUser> userManager,
                                IMapper mapper)
        {
            _ticketService = ticketService;
            _tripService = tripService;
            _userManager = userManager;
            _mapper = mapper;
        }

        [HttpGet]
        public IActionResult Index()
        {
            var tickets = _ticketService.GetUserTickets(Guid.Parse(HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value));
            var ticketsVmList = _mapper.Map<List<TicketViewModel>>(tickets);

            return View("Index", ticketsVmList);
        }

        [HttpPost]
        public IActionResult CancelTicket(int ticketId)
        {
            var tickets = _ticketService.GetAll().Select(t => t.Id).ToList();

            if (tickets.Contains(ticketId))
            {
                _ticketService.CancelTicket(ticketId);

                TempData["OperationMessage"] = "The ticket is canceled";

                return RedirectToAction("Index", "Home");
            }
            else
            {
                return BadRequest();
            }
        }

        [HttpGet]
        public IActionResult BuyTicket(int tripId, Card card)
        {
            card.TripId = tripId;

            return View("BuyTicket", card);
        }

        [HttpPost]
        public IActionResult BuyTicket(Card card)
        {
            var trips = _tripService.GetAll().Select(t => t.Id).ToList();

            if (trips.Contains(card.TripId))
            {
                if (ModelState.IsValid)
                {
                    var userId = Guid.Parse(_userManager.GetUserId(HttpContext.User));
                    var result = _ticketService.BuyTicket(card.TripId, userId);

                    if (result.Result == Result.Success)
                    {
                        TempData["OperationMessage"] = "Ticket purchased";

                        return RedirectToAction("Index");
                    }
                    else
                    {
                        TempData["OperationMessage"] = "Tickets for this flight are sold out";

                        return RedirectToAction("Index");
                    }
                }
                else
                {
                    return View("BuyTicket", card);
                }
            }
            else
            {
                return BadRequest();
            }
        }        
    }
}
