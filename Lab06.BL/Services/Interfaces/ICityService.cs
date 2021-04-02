using Lab06.BL.DTO.CityDTO;
using Lab06.DAL.Entities;
using System.Collections.Generic;

namespace Lab06.BL.Services.Interfaces
{
    public interface ICityService
    {
        List<CityDto> GetAll();
        void AddItem(AddCityDto cityDto);
        List<string> GetCitiesNames();
    }
}
