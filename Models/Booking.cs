using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace MediSight_Project.Models
{
    public class Booking
    {
        public int BookingId { get; set; }
        public string UserId { get; set; }
        public DateTime Time { get; set; }

        [NotMapped]
        public HttpPostedFileBase UploadedFile { get; set; }
    }
}