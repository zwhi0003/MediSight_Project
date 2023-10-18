using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace MediSight_Project.Models
{
    [NotMapped]
    public class BookingUserView
    {
        public List<Booking> Bookings { get; set; }
        public Dictionary<string, string> UserNames { get; set; }
    }
}