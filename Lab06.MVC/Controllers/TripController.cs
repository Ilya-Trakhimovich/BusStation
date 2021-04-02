using AutoMapper;
using Lab06.BL.DTO.TripDTO;
using Lab06.BL.Services.Interfaces;
using Lab06.MVC.Filters.ActionFilters;
using Lab06.MVC.ViewModels.TripViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Linq;

namespace Lab06.MVC.Controllers
{
    [Authorize(Roles = "admin")]
    public class TripController : Controller
    {
        private readonly IBusService _busService;
        private readonly ITripService _tripService;
        private readonly ITicketService _ticketService;
        private readonly ICityService _cityService;
        private readonly IMapper _mapper;

        public TripController(IBusService busService,
                              ITripService tripService,
                              ITicketService ticketService,
                              ICityService cityService,
                              IMapper mapper)
        {
            _busService = busService;
            _tripService = tripService;
            _ticketService = ticketService;
            _cityService = cityService;
            _mapper = mapper;
        }

        [HttpGet]
        public IActionResult AddTrip()
        {
            var addTripViewModel = SetAddTrip(null);

            return View("AddTrip", addTripViewModel);
        }

        [HttpPost]
        [ConvertDateTimeToUtcAttribute]
        public IActionResult AddTrip(TripViewModel trip, int cityId, int busId)
        {
            if (ModelState.IsValid)
            {
                var bus = _busService.GetItemById(busId);
                var tripDto = _mapper.Map<TripDto>(trip);
                tripDto.CityId = cityId;
                tripDto.BusId = busId;
                tripDto.FreeSeats = bus.SeatsCount;

                _tripService.AddTrip(tripDto);
                TempData["OperationMessage"] = "Trip added";

                return RedirectToAction("Index", "Home");
            }
            else
            {
                var addTripViewModel = SetAddTrip(trip);

                return View("AddTrip", addTripViewModel);
            }
        }

        [HttpPost]
        public IActionResult CancelTrip(int tripId)
        {
            _tripService.CancelTrip(tripId);

            TempData["OperationMessage"] = "The trip is canceled";

            return RedirectToAction("Index", "Home");
        }

        [HttpGet]
        public IActionResult UpdateTrip(int tripId)
        {
            var cities = _cityService.GetAll().ToList();
            var citiesView = new SelectList(cities, "Id", "Name", cities[0]);

            var buses = _busService.GetAll().ToList();
            var busesNumbers = new SelectList(buses, "Id", "RegisterNumber", buses[0]);

            var trip = _tripService.GetItemById(tripId);
            var tripViewModel = _mapper.Map<TripViewModel>(trip);

            var updateTripViewModel = new UpdatePageTripViewModel
            {
                Cities = citiesView,
                Buses = busesNumbers,
                Trip = tripViewModel
            };

            return View("UpdateTrip", updateTripViewModel);
        }

        [HttpPost]
        public IActionResult UpdateTrip(TripViewModel trip, int cityId, int busId)
        {
            if (ModelState.IsValid)
            {
                var tripDto = _mapper.Map<TripDto>(trip);
                tripDto.CityId = cityId;
                tripDto.BusId = busId;

                _tripService.UpdateItem(tripDto);
                TempData["OperationMessage"] = "The trip updated";

                return RedirectToAction("Index", "Home");
            }
            else
            {
                var cities = _cityService.GetAll().ToList();
                var citiesView = new SelectList(cities, "Id", "Name");

                var buses = _busService.GetAll().ToList();
                var busesNumbers = new SelectList(buses, "Id", "RegisterNumber");

                var updateTripViewModel = new UpdatePageTripViewModel
                {
                    Cities = citiesView,
                    Buses = busesNumbers,
                    Trip = trip
                };

                return View("UpdateTrip", updateTripViewModel);
            }
        }

        private AddTripPageViewModel SetAddTrip(TripViewModel trip)
        {
            var cities = _cityService.GetAll().ToList();
            var citiesView = new SelectList(cities, "Id", "Name");

            var buses = _busService.GetAll().ToList();
            var busesNumbers = new SelectList(buses, "Id", "RegisterNumber");

            var addTripViewModel = new AddTripPageViewModel
            {
                Cities = citiesView,
                Buses = busesNumbers,
                Trip = trip
            };

            return addTripViewModel;
        }        
    }
}
