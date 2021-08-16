using SharedTripSystem.Data.Models;
using SharedTripSystem.Models.Recommendations;
using System.Collections.Generic;

namespace SharedTripSystem.Services.Recommendations
{
   public interface IRecommendationService
    {
        public IEnumerable<RecommendationsListingModel> All();
        public void Create(string userId, CreateRecommendationFormModel recommendation);
    }
}
