using MyTested.AspNetCore.Mvc;
using SharedTripSystem.Controllers;
using Xunit;

namespace SharedTripSystem.Test.Pipeline
{
   public class RecommendationTests
    {
        [Fact]
        public void CreateTest() => MyMvc
               .Pipeline()
               .ShouldMap(request => request
               .WithPath("/Recommendations/Create")
               .WithUser())
               .To<RecommendationsController>(c => c.Create())
               .Which()
               .ShouldHave()
               .ActionAttributes(x => x.RestrictingForAuthorizedRequests())
               .AndAlso()
               .ShouldReturn()
               .View();
        [Fact]
        public void AllTest() => MyMvc
           .Pipeline()
           .ShouldMap(request => request
           .WithPath("/Recommendations/All")
           .WithUser())
           .To<RecommendationsController>(c => c.All())
           .Which()
           .ShouldHave()
           .ActionAttributes(x => x.RestrictingForAuthorizedRequests())
           .AndAlso()
           .ShouldReturn()
           .View();
    }
}
