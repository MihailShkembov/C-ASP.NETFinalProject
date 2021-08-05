using System.ComponentModel.DataAnnotations;
using static SharedTripSystem.Data.DataConstants.Default;
using static SharedTripSystem.Data.DataConstants.Car;
using static SharedTripSystem.Data.DataConstants.Trip;

namespace SharedTripSystem.Models.Cars
{
    public class AddCarFormModel
    {
        [Required]
        [StringLength(DefaultMaxLength, MinimumLength = DefaultMinLength, ErrorMessage = DefaultErrorMessage)]
        public string Model { get; set; }
        [Required]
        [StringLength(PlateNumberLength,MinimumLength =PlateNumberLength,ErrorMessage =InvalidPlateNumberMessage)]
        public string PlateNumber { get; set; }
        [Required]
        [Range(minimum: KilometersTravelledMinValue, maximum: KilometersTravelledMaxValue, ErrorMessage = InvalidKilometersMessage)]
        public int KilometersTravlled { get; set; }
        [Required]
        [StringLength(DefaultMaxLength, MinimumLength = DefaultMinLength, ErrorMessage = InvalidTypeMessage)]
        public string Type { get; set; }
        [Required]
        [RegularExpression(URLRegex,ErrorMessage =InvalidRegexMessage)]
        public string CarImageUrl { get; set; }
        public string DriverId { get; init; }
    }
}
