using MyTested.AspNetCore.Mvc;
using SharedTripSystem.Controllers;
using SharedTripSystem.Data.Models;
using SharedTripSystem.Models.Recommendations;
using static SharedTripSystem.Test.Data.Trips;
using System.Linq;
using Xunit;

namespace SharedTripSystem.Test.Controllers
{
   public class RecommendationControllerTests
    {
        [Theory]
        [InlineData("Sofia", "Nice")]
        public void PostShouldBeForAuthorizedUsersAndShouldRedirectWithModel
           (string location, string descripton)
       => MyController<RecommendationsController>
           .Instance(controller => controller
           .WithUser())
           .Calling(c => c.Create(new CreateRecommendationFormModel
           {
               Location = location,
               Descripton = descripton
           }))
           .ShouldHave()
           .ActionAttributes(attributes => attributes
           .RestrictingForHttpMethod(HttpMethod.Post)
           .RestrictingForAuthorizedRequests())
           .ValidModelState()
           .Data(data => data
                   .WithSet<Recommendation>(drivers => drivers
                       .Any(d =>
                           d.Location == location &&
                           d.Descripton == descripton)))
            .AndAlso()
               .ShouldReturn()
               .Redirect(redirect => redirect
                   .To<TripsController>(t => t.All(Model)));
    }
}
