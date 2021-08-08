using System;
using System.ComponentModel.DataAnnotations;
using static SharedTripSystem.Data.DataConstants.Default;
using static SharedTripSystem.Data.DataConstants.Trip;
namespace SharedTripSystem.Models.Trips
{
    public class CreateTripFormModel
    {
        [Required]
        [Display(Name = "Start Point")]
        [StringLength(DefaultMaxLength,MinimumLength =DefaultMinLength,ErrorMessage =DefaultErrorMessage)]
        
        public string StartPoint { get; init; }
        [Required]
        [Display(Name = "End Point")]
        [StringLength(DefaultMaxLength, MinimumLength = DefaultMinLength, ErrorMessage = DefaultErrorMessage)]
        public string EndPoint { get; init; }
        [Required]
        [Display(Name = "Free Seats")]
        [Range(FreeSeatsMinCount,FreeSeatsMaxCount)]
        public int FreeSeats { get; init; }
        [Required]
        [Display(Name = "Destination Picture")]
        [RegularExpression(URLRegex,ErrorMessage =InvalidRegexMessage)]
        public string DestinationImageUrl { get; init; }
     
        [Display(Name = "Date and Time of departure")]
        public DateTime DepartureDate { get; init; }
        [MaxLength(DescriptionMaxLength)]
        public string Description { get; set; }
        public string UserId { get; set; }
    }
}
