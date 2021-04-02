using System.Collections.Generic;

namespace Lab06.MVC.ViewModels.StatisticsViewModels
{
    public class TripMonthViewModel
    {
        public List<string> Cities { get; set; } // string[]
        public List<List<int>> TripCountPerMonth { get; set; }
    }
}
