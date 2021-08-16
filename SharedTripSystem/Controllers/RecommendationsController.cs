using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using SharedTripSystem.Data;
using SharedTripSystem.Data.Models;
using SharedTripSystem.Models.Recommendations;
using System.Linq;
using SharedTripSystem.Services.Recommendations;

namespace SharedTripSystem.Controllers
{
    public class RecommendationsController:Controller
    {
        private readonly ApplicationDbContext dbContext;
        private readonly IRecommendationService recommendations;
        public RecommendationsController(ApplicationDbContext dbContext,
            IRecommendationService recommendations)
        {
            this.dbContext = dbContext;
            this.recommendations = recommendations;
        }
        [Authorize]
        public IActionResult All()
        {
            var viewModel = this.recommendations.All();

            return this.View(viewModel);
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
            this.recommendations.Create(userId, recommendation);
            return RedirectToAction("All", "Trips");
        }
    }
}
