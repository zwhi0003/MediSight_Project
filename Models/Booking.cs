using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace MediSight_Project.Models
{

    public class Booking
    {
        public int BookingId { get; set; }
        public string UserId { get; set; }

        [FutureDate(ErrorMessage = "Booking date/time cannot be in the past.")]
        public DateTime Time { get; set; }

        [NotMapped]
        public HttpPostedFileBase UploadedFile { get; set; }
    }

    public class FutureDateAttribute : ValidationAttribute
    {
        public override bool IsValid(object value)
        {
            if (value == null)
            {
                return false;
            }

            if (!(value is DateTime))
            {
                return false;
            }

            var date = (DateTime)value;
            return date >= DateTime.Now;
        }
    }
}