using AutoMapper;
using Lab06.BL.DTO.CityDTO;
using Lab06.BL.DTO.TripDTO;
using Lab06.BL.Services.Interfaces;
using Lab06.DAL.Entities;
using Lab06.MVC.ViewModels.CityViewModels;
using Lab06.MVC.ViewModels.HomeViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Lab06.MVC.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {
        private readonly ICityService _cityService;
        private readonly ITripService _tripService;
        private readonly IMapper _mapper;

        public HomeController(ICityService cityService,
                              ITripService tripService,
                              IMapper mapper)
        {
            _cityService = cityService;
            _tripService = tripService;
            _mapper = mapper;
        }

        public IActionResult Index()
        {
            var mainPageViewModel = GetMainPageViewmodel(1, DateTime.Now.Date);

            return View(mainPageViewModel);
        }

        [HttpPost]
        public IActionResult Index(int cityId, DateTime StartDate)
        {
            var cities = _cityService.GetAll().Select(c => c.Id).ToList();

            if (cities.Contains(cityId))
            {
                var mainPageViewModel = GetMainPageViewmodel(cityId, StartDate);

                return View("Index", mainPageViewModel);
            }
            else
            {
                return BadRequest();
            }
        }

        [AllowAnonymous]
        public IActionResult StationInfo()
        {
            return View();
        }

        private MainPageViewModel GetMainPageViewmodel(int cityId, DateTime date)
        {
            var cities = _cityService.GetAll().ToList() ?? new List<CityDto>();
            var citiesVmList = _mapper.Map<List<CityViewModel>>(cities);
            var citiesView = new SelectList(citiesVmList, "Id", "Name");

            var tripList = _tripService.GetDateTrips(cityId, date).ToList() ?? new List<TripDto>();
            var mainPageTripViewModel = _mapper.Map<List<MainPageTripViewModel>>(tripList);

            return new MainPageViewModel
            {
                Cities = citiesView,
                Trips = mainPageTripViewModel,
                StartDate = date
            };
        }        
    }
}