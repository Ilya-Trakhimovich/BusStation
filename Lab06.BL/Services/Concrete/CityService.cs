using AutoMapper;
using Lab06.BL.DTO.CityDTO;
using Lab06.BL.Services.Interfaces;
using Lab06.DAL.Entities;
using Lab06.DAL.UOW.Interfaces;
using System.Collections.Generic;
using System.Linq;

namespace Lab06.BL.Services.Concrete
{
    public class CityService : ICityService
    {
        private readonly IUnitOfWork _uow;
        private readonly IMapper _mapper;

        public CityService(IUnitOfWork uow, IMapper mapper)
        {
            _uow = uow;
            _mapper = mapper;
        }

        public List<CityDto> GetAll()
        {
            var cityRepo = _uow.GetRepository<City>();
            var cities = cityRepo.GetAll();
            var citiesDto = _mapper.Map<List<CityDto>>(cities);

            return citiesDto;
        }

        public void AddItem(AddCityDto cityDto)
        {
            var cityRepo = _uow.GetRepository<City>();
            var city = _mapper.Map<City>(cityDto);

            cityRepo.Create(city);
            _uow.Save();
        }

        public List<string> GetCitiesNames()
        {
            var cityRepo = _uow.GetRepository<City>();
            var citiesNames = cityRepo.GetAll().Select(c => c.Name).ToList() ?? new List<string>();

            return citiesNames;
        }
    }
}