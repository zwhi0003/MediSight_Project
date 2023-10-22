using System;
using System.ComponentModel.DataAnnotations;

namespace MediSight_Project.Models
{
    public class Review
    {
        public int ReviewId { get; set; }
        public string UserId { get; set; }
        public DateTime Time { get; set; }

        [Required(ErrorMessage = "Review text is required.")]
        public string ReviewText { get; set; }

        [Range(1, 5, ErrorMessage = "Rating must be between 1 and 5.")]
        public int ReviewRating { get; set; }
    }
}