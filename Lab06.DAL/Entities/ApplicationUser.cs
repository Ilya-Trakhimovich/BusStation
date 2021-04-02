using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;

namespace Lab06.DAL.Entities
{
    public class ApplicationUser : IdentityUser
    {
        public virtual List<Ticket> Tickets { get; set; }

        public ApplicationUser()
        {
            Tickets = new List<Ticket>();
        }
    }
}
