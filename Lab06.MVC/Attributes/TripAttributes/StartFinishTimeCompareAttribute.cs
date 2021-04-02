using Lab06.MVC.ViewModels.TripViewModels;
using System.ComponentModel.DataAnnotations;

namespace Lab06.MVC.Attributes.TripAttributes
{
    public class StartFinishTimeCompareAttribute : ValidationAttribute
    {
        public StartFinishTimeCompareAttribute()
        {
            ErrorMessage = "Finish time must be greater than start time for the same day";
        }

        public override bool IsValid(object value)
        {
            TripViewModel trip = value as TripViewModel;

            if (trip.FinishDate == trip.StartDate && trip.FinishTip > trip.StartTrip)
            {
                return true;
            }
            else if (trip.FinishDate > trip.StartDate)
            {
                return true;
            }

            return false;
        }
    }
}
