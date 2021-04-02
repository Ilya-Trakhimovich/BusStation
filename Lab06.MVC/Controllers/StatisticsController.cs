using Lab06.BL.Services.Interfaces;
using Lab06.MVC.ViewModels.StatisticsViewModels;
using Microsoft.AspNetCore.Mvc;

namespace Lab06.MVC.Controllers
{
    public class StatisticsController : Controller
    {
        private readonly ICityService _cityService;
        private readonly ITripService _tripService;

        public StatisticsController(ICityService cityService, ITripService tripService)
        {
            _cityService = cityService;
            _tripService = tripService;
        }

        public IActionResult Statistics()
        {
            return View();
        }

        public IActionResult AverageCityTripCostPerMonth()
        {
            var cities = _cityService.GetCitiesNames();
            var average = _tripService.GetAverageCityTripCostPerMonth();

            var averageCityTripCostVm = new AverageTripCostPerMonthViewModel
            {
                AverageCost = average,
                Cities = cities
            };

            return View(averageCityTripCostVm);
        }

        public IActionResult CityTripsPerMonthStatistics()
        {
            var cities = _cityService.GetCitiesNames();
            var count = _tripService.GetListCityTripsCountPerMonths();

            var tripMonthVm = new TripMonthViewModel
            {
                Cities = cities,
                TripCountPerMonth = count
            };

            return View("CityTripsPerMonthStatistics", tripMonthVm);
        }       
    }
}
