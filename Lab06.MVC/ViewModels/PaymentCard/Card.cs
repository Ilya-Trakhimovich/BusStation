using Lab06.MVC.Attributes.CardAttributes;
using System.ComponentModel.DataAnnotations;

namespace Lab06.MVC.ViewModels.PaymentCard
{
    [CheckCurrentMonth]
    public class Card
    {
        public int TripId { get; set; }

        [Display(Name = "Credit number")]
        [Required(ErrorMessage = "Credit number is required")]        
        [IsDigit]
        [StringLength(19, MinimumLength = 19, ErrorMessage = "Credit number must have 16 digits")]
        public string CreditNumber { get; set; }

        [Display(Name = "Verification number")]
        [Required(ErrorMessage = "Verification number is required")]
        [IsDigit]
        [StringLength(3, MinimumLength = 3, ErrorMessage = "Verification number must have 3 digits")]        
        public string VerificationNumber { get; set; }

        [Required(ErrorMessage = "Month is required")]
        [IsDigit]
        [StringLength(2, MinimumLength = 1, ErrorMessage = "Enter month from 1 to 12")]  
        [MonthRange]       
        public string Month { get; set; }

        [Required(ErrorMessage = "Year is required")]
        [StringLength(4, MinimumLength = 4, ErrorMessage = "Year must have full format (yyyy)")]
        [IsDigit]
        [ExpirationCardYear]
        public string Year { get; set; }
    }
}
