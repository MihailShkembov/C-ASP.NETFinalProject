using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using static SharedTripSystem.Data.DataConstants.Default;

namespace SharedTripSystem.Data.Models
{
    public class Driver
    {
        public string Id { get; init; } = Guid.NewGuid().ToString();
        [Required]
        public string FullName { get; set; }
        [Required]
        [MaxLength(DefaultMaxLength)]
        public string  DriversLicense { get; set; }
        [Required]
        public string UserId { get; set; }

        public List<Car> Cars { get; set; } = new List<Car>();
    }
}
