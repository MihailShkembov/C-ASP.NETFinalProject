using MyTested.AspNetCore.Mvc;
using SharedTripSystem.Controllers;
using static SharedTripSystem.Test.Data.Cars;
using Xunit;

namespace SharedTripSystem.Test.Pipeline
{
   public class CarsControllerTests
    {
        [Fact]
        public void CreateTest() => MyMvc
                .Pipeline()
                .ShouldMap(request => request
                .WithPath("/Cars/Create")
                .WithUser())
                .To<CarsController>(c => c.Create())
                .Which()
                .ShouldHave()
                .ActionAttributes(x => x.RestrictingForAuthorizedRequests())
                .AndAlso()
                .ShouldReturn()
                .View();
        [Fact]
        public void DeleteTest()
            => MyMvc
            .Pipeline()
            .ShouldMap(request => request
            .WithUser()
            .WithPath("/Cars/Delete")
            .WithQueryString($"?carId={CarId}"))
            .To<CarsController>(c => c.Delete(CarId))
            .Which()
            .ShouldHave()
            .ActionAttributes(attribute => attribute
            .RestrictingForAuthorizedRequests())
            .AndAlso()
            .ShouldReturn()
            .RedirectToAction("Index", "Home");
            
            
        
    }
}
