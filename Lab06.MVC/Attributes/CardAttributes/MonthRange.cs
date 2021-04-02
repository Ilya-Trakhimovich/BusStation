using System;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace Lab06.MVC.Attributes.CardAttributes
{
    public class MonthRangeAttribute : ValidationAttribute
    {
        public MonthRangeAttribute()
        {
            ErrorMessage = "Enter month from 1 to 12";
        }

        public override bool IsValid(object value)
        {
            string tempValue = value as string;
            string[] allowValues = new string[12];

            for (var i = 1; i <= allowValues.Length; i++)
            {
                allowValues[i-1] = i.ToString();
            }

            if (allowValues.Contains(tempValue))
            {
                return true;
            }

            return false;
        }
    }
}
