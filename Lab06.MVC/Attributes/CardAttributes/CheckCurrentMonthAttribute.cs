using Lab06.MVC.ViewModels.PaymentCard;
using System;
using System.ComponentModel.DataAnnotations;

namespace Lab06.MVC.Attributes.CardAttributes
{
    public class CheckCurrentMonthAttribute : ValidationAttribute
    {
        public CheckCurrentMonthAttribute()
        {
            ErrorMessage = "Wrong month. Your card has expirated";
        }

        public override bool IsValid(object value)
        {
            Card card = value as Card;
            Int32.TryParse(card.Year, out int year);
            Int32.TryParse(card.Month, out int month);

            if (year == DateTime.Now.Year && month < DateTime.Now.Month)
            {
                return false;
            }

            return true;
        }
    }
}