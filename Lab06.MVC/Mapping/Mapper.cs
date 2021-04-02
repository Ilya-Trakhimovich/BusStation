using AutoMapper;
using Lab06.BL.DTO.CityDTO;
using Lab06.BL.DTO.TicketDTO;
using Lab06.BL.DTO.TripDTO;
using Lab06.MVC.ViewModels.CityViewModels;
using Lab06.MVC.ViewModels.HomeViewModels;
using Lab06.MVC.ViewModels.TicketViewModels;
using Lab06.MVC.ViewModels.TripViewModels;

namespace Lab06.MVC.Mapping
{
    public class Mapper : Profile
    {
        public Mapper()
        {
            CreateMap<AddCityViewModel, AddCityDto>();
            CreateMap<TripViewModel, TripDto>().ReverseMap();
            CreateMap<CityViewModel, CityDto>().ReverseMap();

            CreateMap<TripDto, MainPageTripViewModel>()
                .ForMember(dest => dest.StartTime,
                            opt => opt.MapFrom(src => src.StartTrip))
                .ForMember(dest => dest.FinishTime,
                            opt => opt.MapFrom(src => src.FinishTip))
                .ReverseMap();

            CreateMap<TicketDto, TicketViewModel>()
                .ForMember(dest => dest.Cost,
                           opt => opt.MapFrom(src => src.Trip.Cost))
                .ForMember(dest => dest.StartDate,
                           opt => opt.MapFrom(src => src.Trip.StartDate))
                .ForMember(dest => dest.StartTime,
                           opt => opt.MapFrom(src => src.Trip.StartTrip))
                .ForMember(dest => dest.FinishDate,
                           opt => opt.MapFrom(src => src.Trip.FinishDate))
                .ForMember(dest => dest.FinishTime,
                           opt => opt.MapFrom(src => src.Trip.FinishTip))
                .ForMember(dest => dest.City,
                           opt => opt.MapFrom(src => src.Trip.City.Name));
        }
    }
}
