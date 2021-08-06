using System;
using System.ComponentModel.DataAnnotations;
using static SharedTripSystem.Data.DataConstants.Default;
using static SharedTripSystem.Data.DataConstants.Trip;

namespace SharedTripSystem.Data.Models
{
    public class Trip
    {
        private bool hasExpired = false;
        public string Id { get; set; } = Guid.NewGuid().ToString();
        [Required]
        [MaxLength(DefaultMaxLength)]
        public string StartPoint { get; set; }
        [Required]
        [MaxLength(DefaultMaxLength)]
        public string EndPoint { get; set; }
        [Range(FreeSeatsMinCount,FreeSeatsMaxCount)]
        public int FreeSeats { get; set; }
        [Required]
        public string DestinationImageUrl { get; set; }
        public string Description { get; set; }
        public bool HasExpired
        {
            get
            {
                if (this.DepartureDate>DateTime.UtcNow)
                {
                    return true;
                }
                return this.hasExpired;
            }
        }
        public Car Car { get; set; }
        public string  CarId { get; set; }
        public DateTime DepartureDate { get; set; }

    }
}
