using System;
using System.ComponentModel.DataAnnotations;
using static SharedTripSystem.Data.DataConstants.Default;
namespace SharedTripSystem.Data.Models
{
    public class Recommendation
    {
        public string Id { get; set; } = Guid.NewGuid().ToString();
        [Required]
        public string UserId { get; set; }
        public User User { get; set; }

        [Required]
        [MaxLength(DefaultMaxLength)]
        public string Location { get; set; }
        [Required]
        public string Descriptop { get; set; }

    }
}
