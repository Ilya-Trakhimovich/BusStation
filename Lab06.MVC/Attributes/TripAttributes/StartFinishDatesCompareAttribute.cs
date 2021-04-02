using Lab06.MVC.ViewModels.TripViewModels;
using System.ComponentModel.DataAnnotations;

namespace Lab06.MVC.Attributes.TripAttributes
{
    public class StartFinishDatesCompareAttribute : ValidationAttribute
    {
        public StartFinishDatesCompareAttribute()
        {
            ErrorMessage = "Finish date must be greater than start date";
        }

        public override bool IsValid(object value)
        {
            TripViewModel trip = value as TripViewModel;

            if (trip.FinishDate >= trip.StartDate)
            {
                return true;
            }

            return false;
        }
    }
}
