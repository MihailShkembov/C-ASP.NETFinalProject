using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using static SharedTripSystem.Data.DataConstants.Default;
using static SharedTripSystem.Data.DataConstants.Trip;

namespace SharedTripSystem.Data.Models
{
    public class Trip
    {
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
        
        public string UserId { get; set; }
        public User User { get; set; }
        public Car Car { get; set; }
        public string  CarId { get; set; }
        public DateTime DepartureDate { get; set; }
        public ICollection<User> Users = new List<User>();

    }
}
