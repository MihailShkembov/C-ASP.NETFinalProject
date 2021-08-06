using System.ComponentModel.DataAnnotations;
using static SharedTripSystem.Data.DataConstants.Default;

namespace SharedTripSystem.Models.Recommendations
{
    public class CreateRecommendationFormModel
    {
        [Required]
        [StringLength(DefaultMaxLength,MinimumLength =DefaultMinLength,ErrorMessage =DefaultErrorMessage)]
        public string Location { get; set; }
        [Required]
        public string Descripton { get; set; }
    }
}
