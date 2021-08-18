using MyTested.AspNetCore.Mvc;
using SharedTripSystem.Controllers;
using Xunit;

namespace SharedTripSystem.Test.Pipeline
{
   public class DriversControllerTests
    {
        [Fact]
        public void CreateTest()
        {
            MyMvc
                .Pipeline()
                .ShouldMap(request => request
                .WithPath("/Drivers/Create")
                .WithUser())
                .To<DriversController>(c => c.Create())
                .Which()
                .ShouldHave()
                .ActionAttributes(x => x.RestrictingForAuthorizedRequests())
                .AndAlso()
                .ShouldReturn()
                .View();
        }
    }
}
