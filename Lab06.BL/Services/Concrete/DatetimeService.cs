using Lab06.BL.Services.Interfaces;
using System;

namespace Lab06.BL.Services.Concrete
{
    public class DatetimeService : IDatetimeService
    {
        public DateTime GetLocalDatetimeNow()
        {
            return DateTime.Now;
        }

        public DateTime GetUtcDatetimeNow()
        {
            return DateTime.UtcNow;
        }
    }
}
