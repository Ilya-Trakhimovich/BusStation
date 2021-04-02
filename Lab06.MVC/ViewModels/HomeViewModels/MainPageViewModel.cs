using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Lab06.MVC.ViewModels.HomeViewModels
{
    public class MainPageViewModel
    {
        public SelectList Cities { get; set; }
        public List<MainPageTripViewModel> Trips { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd.MM.yyyy}", ApplyFormatInEditMode = true)]
        public DateTime StartDate { get; set; }
    }
}
