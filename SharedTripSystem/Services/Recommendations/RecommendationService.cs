using SharedTripSystem.Data;
using SharedTripSystem.Data.Models;
using SharedTripSystem.Models.Recommendations;
using System.Collections.Generic;
using System.Linq;

namespace SharedTripSystem.Services.Recommendations
{
    public class RecommendationService:IRecommendationService
    {
        private readonly ApplicationDbContext dbContext;
        public RecommendationService(ApplicationDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public IEnumerable<RecommendationsListingModel> All()
        {
            var recommendations = this.dbContext.Recommendations
               .ToList()
               .Select(x => new RecommendationsListingModel
               {
                   Location = x.Location,
                   Description = x.Descripton
               })
               .ToList();
            return recommendations;
        }

        public void Create(string userId,CreateRecommendationFormModel recommendation)
        {
            var recommendationToAdd = new Recommendation
            {
                UserId = userId,
                Location = recommendation.Location,
                Descripton = recommendation.Descripton
            };
            this.dbContext.Recommendations.Add(recommendationToAdd);
            this.dbContext.SaveChanges();
        }
    }
}
