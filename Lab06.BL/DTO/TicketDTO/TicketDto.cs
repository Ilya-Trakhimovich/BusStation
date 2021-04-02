using Lab06.BL.DTO.TripDTO;

namespace Lab06.BL.DTO.TicketDTO
{
    public class TicketDto
    {
        public int Id { get; set; }
        public bool IsCanceled { get; set; }
        public string ApplicationUserId { get; set; }
        public int TripId { get; set; }
        public TripDto Trip { get; set; }
    }
}
