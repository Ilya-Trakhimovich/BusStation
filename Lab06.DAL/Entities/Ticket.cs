namespace Lab06.DAL.Entities
{
    public class Ticket
    {
        public int Id { get; set; }
        public bool IsCanceled { get; set; }
        public string ApplicationUserId { get; set; }
        public virtual ApplicationUser ApplicationUser { get; set; }
        public int TripId { get; set; }
        public virtual Trip Trip { get; set; }
    }
}
