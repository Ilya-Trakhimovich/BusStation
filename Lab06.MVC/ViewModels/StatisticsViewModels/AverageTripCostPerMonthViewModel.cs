using System.Collections.Generic;

namespace Lab06.MVC.ViewModels.StatisticsViewModels
{
    public class AverageTripCostPerMonthViewModel
    {
        public List<string> Cities { get; set; }
        public List<List<double>> AverageCost { get; set; }
    }
}
