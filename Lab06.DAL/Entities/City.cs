using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Lab06.DAL.Entities
{
    public class City
    {
        public int Id { get; set; }

        [Display(Name = "City")]
        public string Name { get; set; }
        public virtual List<Trip> Trips { get; set; }
        public City()
        {
            Trips = new List<Trip>();
        }
    }
}
