using AutoMapper;
using Lab06.BL.DTO.TripDTO;
using Lab06.BL.Services.Interfaces;
using Lab06.DAL.Entities;
using Lab06.DAL.UOW.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Lab06.BL.Services.Concrete
{
    public class TripService : ITripService
    {
        private readonly IUnitOfWork _uow;
        private readonly IMapper _mapper;
        private readonly IDatetimeService _dtService;

        public TripService(IUnitOfWork uow,
                           IMapper mapper,
                           IDatetimeService dtService)
        {
            _uow = uow;
            _mapper = mapper;
            _dtService = dtService;
        }

        public List<DateTime> GetActualDates()
        {
            var trips = _uow.GetRepository<Trip>().GetAll().ToList();
            var dates = trips.Where(d => (d.StartDate + d.StartTrip) >= _dtService.GetUtcDatetimeNow())
                             .Select(d => d.StartDate.Date)
                             .Distinct()
                             .OrderBy(d => d.Date)
                             .ToList();

            return dates;
        }

        public List<TripDto> GetDateTrips(int cityId, DateTime date)
        {
            if (date.Date < _dtService.GetLocalDatetimeNow().Date) // Compare with the current date. Trips in past are not selected. Current date doesnt depend on local time
            {
                return new List<TripDto>();
            }

            var trips = _uow.GetRepository<Trip>().GetAll().ToList();
            var tempDateTrips = trips.Where(t => (t.CityId == cityId
                                              && (t.StartDate + t.StartTrip).ToLocalTime().Date == date.Date))
                                     .OrderBy(t => t.StartTrip)
                                     .ToList();

            foreach (var d in tempDateTrips)
            {
                var startLocalDate = (d.StartDate + d.StartTrip).ToLocalTime();
                var finishLocalDate = (d.FinishDate + d.FinishTip).ToLocalTime();

                d.StartDate = startLocalDate.Date;
                d.StartTrip = startLocalDate.TimeOfDay;
                d.FinishDate = finishLocalDate.Date;
                d.FinishTip = finishLocalDate.TimeOfDay;
            }

            if (date.Date == _dtService.GetLocalDatetimeNow().Date)
            {
                var todayTrips = tempDateTrips.Where(d => (d.StartDate + d.StartTrip).ToLocalTime().TimeOfDay > _dtService.GetLocalDatetimeNow().TimeOfDay).ToList();
                var todayTripsDto = _mapper.Map<List<TripDto>>(todayTrips);

                return todayTripsDto;
            }

            var tempDateTripsDto = _mapper.Map<List<TripDto>>(tempDateTrips);

            return tempDateTripsDto;
        }

        public List<TripDto> GetAll()
        {
            var trips = _uow.GetRepository<Trip>().GetAll().ToList();
            var tripsDto = _mapper.Map<List<TripDto>>(trips);

            return tripsDto;
        }

        public void AddTrip(TripDto tripDto)
        {
            var tripRepo = _uow.GetRepository<Trip>();
            var trip = _mapper.Map<Trip>(tripDto);

            tripRepo.Create(trip);
            _uow.Save();
        }

        public void UpdateItem(TripDto tripDto)
        {
            var tripRepo = _uow.GetRepository<Trip>();
            var trip = _mapper.Map<Trip>(tripDto);

            tripRepo.Update(trip);
            _uow.Save();
        }

        public TripDto GetItemById(int tripId)
        {
            var trip = _uow.GetRepository<Trip>().GetItemById(tripId) ?? new Trip();

            var startLocalDate = (trip.StartDate + trip.StartTrip).ToLocalTime();
            var finishLocalDate = (trip.FinishDate + trip.FinishTip).ToLocalTime();

            trip.StartDate = startLocalDate.Date;
            trip.StartTrip = startLocalDate.TimeOfDay;
            trip.FinishDate = finishLocalDate.Date;
            trip.FinishTip = finishLocalDate.TimeOfDay;

            var tripDto = _mapper.Map<TripDto>(trip);

            return tripDto;
        }

        public List<List<int>> GetListCityTripsCountPerMonths()
        {
            var tripRepo = _uow.GetRepository<Trip>();

            var trips = tripRepo.GetAll().ToList() ?? new List<Trip>();
            var cityTripsPerYear = tripRepo.GetAll().GroupBy(t => t.CityId);
            var citiesId = tripRepo.GetAll().Select(c => c.CityId).Distinct().ToList() ?? new List<int>();

            var numberTripsPerMonth = new List<List<int>>();

            for (var i = 0; i < cityTripsPerYear.Count(); i++)
            {
                var listTrips = new List<int>();

                for (var j = 1; j <= 12; j++) // j = number of months
                {
                    var tripPerMonth = trips.Count(t => ((t.StartDate + t.StartTrip).Month == j && t.CityId == citiesId[i]));
                    listTrips.Add(tripPerMonth);
                }

                numberTripsPerMonth.Add(listTrips);
            }

            return numberTripsPerMonth;
        }

        public List<List<double>> GetAverageCityTripCostPerMonth()
        {
            var tripRepo = _uow.GetRepository<Trip>();

            var trips = tripRepo.GetAll().ToList() ?? new List<Trip>();
            var cityTripsPerYear = tripRepo.GetAll().GroupBy(t => t.CityId);
            var citiesId = tripRepo.GetAll().Select(c => c.CityId).Distinct().ToList() ?? new List<int>();

            var listAverageCityTripsCostPerMonth = new List<List<double>>();

            for (var i = 0; i < cityTripsPerYear.Count(); i++)
            {
                var listTrips = new List<double>();

                for (var j = 1; j <= 12; j++) // j = number of months
                {
                    var tripPerMonth = trips.Where(t => ((t.StartDate + t.StartTrip).Month == j && t.CityId == citiesId[i])).ToList();
                    var average = tripPerMonth.Average(c => c?.Cost) ?? 0;
                    listTrips.Add(average);
                }

                listAverageCityTripsCostPerMonth.Add(listTrips);
            }

            return listAverageCityTripsCostPerMonth;
        }

        /// <summary>
        /// Method removes trip and tickets sold for that trip
        /// </summary>
        /// <param name="tripId"></param>
        public void CancelTrip(int tripId)
        {
            var tripRepo = _uow.GetRepository<Trip>();
            var ticketRepo = _uow.GetRepository<Ticket>();

            var trip = tripRepo.GetItemById(tripId);
            var tickets = ticketRepo.GetAll().Where(t => t.TripId == trip.Id).ToList();

            trip.IsCanceled = true;
            tripRepo.Update(trip);

            foreach (var ticket in tickets)
            {
                ticket.IsCanceled = true;
                ticket.Trip.FreeSeats++;

                ticketRepo.Update(ticket);
            }

            _uow.Save();
        }        
    }
}