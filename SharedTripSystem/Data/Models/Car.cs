using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using static SharedTripSystem.Data.DataConstants.Car;
using static SharedTripSystem.Data.DataConstants.Default;

namespace SharedTripSystem.Data.Models
{
    public class Car
    {
        public string Id { get; set; } = Guid.NewGuid().ToString();
        [Required]
        [MaxLength(PlateNumberLength)]
        public string PlateNumber { get; set; } 
        [Required]
        public int KilometersTravlled { get; set; }
        [MaxLength(DefaultMaxLength)]
        [Required]
        public string Type { get; set; }
        public string DriverId { get; init; }

        public Driver Driver { get; init; }
        public ICollection<Trip> Trips { get; set; } = new List<Trip>();

    }
}