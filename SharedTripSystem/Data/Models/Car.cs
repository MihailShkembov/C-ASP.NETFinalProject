using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using static SharedTripSystem.Data.DataConstants;

namespace SharedTripSystem.Data.Models
{
    public class Car
    {
        public Car()
        {
            this.Trips = new HashSet<Trip>();
        }
        public string Id { get; set; } = Guid.NewGuid().ToString();
        [Required]
        [MaxLength(PlateNumberLength)]
        public string PlateNumber { get; set; } 
        [Required]
        public int KilometersTravlled { get; set; }
        [MaxLength(DefaultMaxLength)]
        [Required]
        public string Type { get; set; }
        public ICollection<Trip> Trips { get; set; }

    }
}