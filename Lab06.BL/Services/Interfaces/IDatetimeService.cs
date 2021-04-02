using System;

namespace Lab06.BL.Services.Interfaces
{
    public interface IDatetimeService
    {
        DateTime GetLocalDatetimeNow();
        DateTime GetUtcDatetimeNow();
    }
}
