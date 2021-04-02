using Lab06.BL.DTO.TripDTO;
using Lab06.DAL.Entities;
using System;
using System.Collections.Generic;

namespace Lab06.BL.Services.Interfaces
{
    public interface ITripService
    {
        List<DateTime> GetActualDates();
        List<TripDto> GetDateTrips(int cityId, DateTime date);
        List<TripDto> GetAll();
        void AddTrip(TripDto tripDto);
        void UpdateItem(TripDto tripDto);
        TripDto GetItemById(int tripId);
        List<List<int>> GetListCityTripsCountPerMonths();
        List<List<double>> GetAverageCityTripCostPerMonth();
        void CancelTrip(int tripId);
    }
}
