using AutoMapper;
using Lab06.BL.DTO.BusDTO;
using Lab06.BL.DTO.CityDTO;
using Lab06.BL.DTO.TicketDTO;
using Lab06.BL.DTO.TripDTO;
using Lab06.DAL.Entities;

namespace Lab06.BL.Mapping
{
    public class Mapper : Profile
    {
        public Mapper()
        {
            CreateMap<AddCityDto, City>();
            CreateMap<CityDto, City>().ReverseMap();
            CreateMap<Ticket, TicketDto>().ReverseMap();
            CreateMap<Bus, BusDto>().ReverseMap();
            CreateMap<Trip, TripDto>().ReverseMap();
        }
    }
}
