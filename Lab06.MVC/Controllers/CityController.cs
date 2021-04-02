using AutoMapper;
using Lab06.BL.DTO.CityDTO;
using Lab06.BL.Services.Interfaces;
using Lab06.MVC.ViewModels.CityViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Lab06.MVC.Controllers
{
    //[Authorize(Roles = "admin")]
    public class CityController : Controller
    {
        private readonly ICityService _cityService;
        private readonly IMapper _mapper;

        public CityController(ICityService cityService, IMapper mapper)
        {
            _cityService = cityService;
            _mapper = mapper;
        }

        [HttpGet]
        public IActionResult AddCity()
        {
            return View();
        }

        [HttpPost]
        public IActionResult AddCity(AddCityViewModel city)
        {
            var cityNames = _cityService.GetCitiesNames();

            if (ModelState.IsValid && !string.IsNullOrEmpty(city.Name) && !cityNames.Contains(city.Name))
            {
                var cityDto = _mapper.Map<AddCityDto>(city);

                _cityService.AddItem(cityDto);
                TempData["OperationMessage"] = $"City {city.Name} added";

                return RedirectToAction("Index", "Home");
            }
            else
            {
                if (string.IsNullOrEmpty(city.Name))
                {
                    ModelState.AddModelError("", "Empty name");

                    return View("AddCity", city);
                }

                ModelState.AddModelError("", "The city already exist");

                return View("AddCity", city);
            }
        }        
    }
}
