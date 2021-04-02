using Lab06.MVC.ViewModels.TripViewModels;
using System;
using System.ComponentModel.DataAnnotations;

namespace Lab06.MVC.Attributes.TripAttributes
{
    public class DisableTripInPastAttribute : ValidationAttribute
    {
        public DisableTripInPastAttribute()
        {
            ErrorMessage = "Impossible to create flight in the past";
        }

        public override bool IsValid(object value)
        {
            TripViewModel trip = value as TripViewModel;
            DateTime tripFullDate = trip.StartDate + trip.StartTrip;

            if (tripFullDate < DateTime.UtcNow)
            {
                return false;
            }

            return true;
        }
    }
}
