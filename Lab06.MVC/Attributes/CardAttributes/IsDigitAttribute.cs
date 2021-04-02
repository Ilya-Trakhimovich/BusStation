using System;
using System.ComponentModel.DataAnnotations;

namespace Lab06.MVC.Attributes.CardAttributes
{
    public class IsDigitAttribute : ValidationAttribute
    {
        public IsDigitAttribute()
        {
            ErrorMessage = "The field must be a number";
        }

        public override bool IsValid(object value)
        {
            string tempValue = value as string;

            if (value != null && Int64.TryParse(tempValue.Replace(" ", ""), out long res))
            {
                return true;
            }

            return false;
        }
    }
}
