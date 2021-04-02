using System;
using System.ComponentModel.DataAnnotations;

namespace Lab06.MVC.ViewModels.TicketViewModels
{
    public class TicketViewModel
    {
        public int Id { get; set; }
        public string City { get; set; }

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
        public TimeSpan StartTime { get; set; }

        [Display(Name = "Finish")]
        [DataType(DataType.Time)]
        [DisplayFormat(DataFormatString = "{0:hh\\:mm}", ApplyFormatInEditMode = true)]
        public TimeSpan FinishTime { get; set; }
        public int Cost { get; set; }
        public bool IsCanceled { get; set; }
    }
}
