using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Lab06.DAL.Entities
{
    public class Bus
    {
        public int Id { get; set; }

        [Display(Name = "Bus")]
        public string RegisterNumber { get; set; }
        public int SeatsCount { get; set; }        

        public virtual List<Trip> Trips { get; set; }

        public Bus()
        {
            Trips = new List<Trip>();
        }
    }
}
