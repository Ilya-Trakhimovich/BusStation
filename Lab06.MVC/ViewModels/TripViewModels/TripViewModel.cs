using Lab06.MVC.Attributes.TripAttributes;
using System;
using System.ComponentModel.DataAnnotations;

namespace Lab06.MVC.ViewModels.TripViewModels
{
    [StartFinishDatesCompare]
    [StartFinishTimeCompare]
    [DisableTripInPast]
    public class TripViewModel
    {
        public int Id { get; set; }

        [Display(Name = "Start date")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd.MM.yyyy}", ApplyFormatInEditMode = true)]
        public DateTime StartDate { get; set; }

        [Display(Name = "Finish date")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd.MM.yyyy}", ApplyFormatInEditMode = true)]
        public DateTime FinishDate { get; set; }

        [Display(Name = "Start")]
        [DataType(DataType.Time)]
        [DisplayFormat(DataFormatString = "{0:hh\\:mm}", ApplyFormatInEditMode = true)]
        public TimeSpan StartTrip { get; set; }

        [Display(Name = "Finish")]
        [DataType(DataType.Time)]
        [DisplayFormat(DataFormatString = "{0:hh\\:mm}", ApplyFormatInEditMode = true)]
        public TimeSpan FinishTip { get; set; }

        [Range(1, 20)]
        public int Cost { get; set; }
        public int FreeSeats { get; set; }
        public int CountSoldSeats { get; set; }
        public int BusId { get; set; }
        public int CityId { get; set; }
    }
}
