using System.ComponentModel.DataAnnotations;
using static SharedTripSystem.Data.DataConstants.Default;

namespace SharedTripSystem.Models.Drivers
{
    public class BecomeDriverFormModel
    {
        [Required]
        public string FullName { get; set; }
        [Required]
        [MaxLength(DefaultMaxLength)]
        public string DriversLicense { get; set; }
    }
}
