using Lab06.BL.DTO.CityDTO;
using System;

namespace Lab06.BL.DTO.TripDTO
{
    public class TripDto
    {
        public int Id { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime FinishDate { get; set; }
        public TimeSpan StartTrip { get; set; }
        public TimeSpan FinishTip { get; set; }
        public int Cost { get; set; }
        public int FreeSeats { get; set; }
        public int CountSoldSeats { get; set; }
        public int BusId { get; set; }
        public bool IsCanceled { get; set; }
        public int CityId { get; set; }
        public CityDto City { get; set; }
    }
}
