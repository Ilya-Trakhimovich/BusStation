using System;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace Lab06.MVC.Attributes.CardAttributes
{
    public class ExpirationCardYearAttribute : ValidationAttribute
    {
        public ExpirationCardYearAttribute()
        {
            ErrorMessage = "Enter year from 2021 to 2031";
        }

        public override bool IsValid(object value)
        {
            string tempValue = value as string;

            string[] allowValues = { "2021", "2022", "2023", "2024", "2025", "2026", "2027", "2028", "2029", "2030", "2031" };

            if (allowValues.Contains(tempValue))
            {
                return true;
            }

            return false;
        }
    }
}
