using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using static SharedTripSystem.Data.DataConstants;
namespace SharedTripSystem.Data.Models
{
    public class Car
    {
        [Key]
        [Required]
        [MaxLength(DefaultMaxLength)]
        public string PlateNumber { get; set; } 
        public int KilometersTravlled { get; set; }
        [Required]
        [MaxLength(DefaultMaxLength)]
        public string Type { get; set; }
        public string Description { get; set; }
        public ICollection<Trip> Trips { get; set; } = new List<Trip>();

    }
}