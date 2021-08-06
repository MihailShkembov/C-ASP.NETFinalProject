using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using SharedTripSystem.Data;
using SharedTripSystem.Data.Models;
using SharedTripSystem.Models.Recommendations;
using System.Linq;

namespace SharedTripSystem.Controllers
{
    public class RecommendationsController:Controller
    {
        private readonly ApplicationDbContext dbContext;
        public RecommendationsController(ApplicationDbContext dbContext)
        {
            this.dbContext = dbContext;
        }
        [Authorize]
        public IActionResult All()
        {
            var recommendations = this.dbContext.Recommendations
                .ToList()
                .Select(x => new AllRecommendationsListingModel
                {
                    Location = x.Location,
                    Description = x.Descripton
                })
                .ToList();

            return this.View(recommendations);
        }
        [Authorize]
        public IActionResult Create() => this.View();
        [HttpPost]
        [Authorize]
        public IActionResult Create(CreateRecommendationFormModel recommendation)
        {
            if (!ModelState.IsValid)
            {
                return this.View(recommendation);
            }
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var recommendationToAdd = new Recommendation
            {
                UserId = userId,
                Location = recommendation.Location,
                Descripton = recommendation.Descripton
            };
            this.dbContext.Recommendations.Add(recommendationToAdd);
            this.dbContext.SaveChanges();
            return RedirectToAction("All", "Trips");
        }
    }
}
