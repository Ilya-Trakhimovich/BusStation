using Lab06.MVC.ViewModels.TripViewModels;
using Microsoft.AspNetCore.Mvc.Filters;
using System;

namespace Lab06.MVC.Filters.ActionFilters
{
    public class ConvertDateTimeToUtcAttribute : Attribute, IActionFilter
    {
        public void OnActionExecuted(ActionExecutedContext context)
        {
            // The method is not used because there is no needed to change/modify/update after executed action
        }

        public void OnActionExecuting(ActionExecutingContext context)
        {
            if (context.ModelState.IsValid)
            {
                var args = context.ActionArguments;
                var trip = args["trip"] as TripViewModel;

                var startDate = trip.StartDate;
                var startTime = trip.StartTrip;
                var finishDate = trip.FinishDate;
                var finishTime = trip.FinishTip;

                var utcStartDatetime = (startDate + startTime).ToUniversalTime();
                var utcFinishDatetime = (finishDate + finishTime).ToUniversalTime();

                trip.StartDate = utcStartDatetime.Date;
                trip.StartTrip = utcStartDatetime.TimeOfDay;
                trip.FinishDate = utcFinishDatetime.Date;
                trip.FinishTip = utcFinishDatetime.TimeOfDay;
            }
        }
    }
}
