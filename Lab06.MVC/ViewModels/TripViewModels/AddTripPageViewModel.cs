using Microsoft.AspNetCore.Mvc.Rendering;

namespace Lab06.MVC.ViewModels.TripViewModels
{
    public class AddTripPageViewModel
    {
        public SelectList Cities { get; set; }
        public SelectList Buses { get; set; }
        public TripViewModel Trip { get; set; }
    }
}
