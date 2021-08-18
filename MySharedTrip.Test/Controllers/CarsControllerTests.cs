using Xunit;
using MyTested.AspNetCore.Mvc;
using SharedTripSystem.Controllers;
using SharedTripSystem.Models.Cars;
using SharedTripSystem.Data.Models;
using System.Linq;

namespace SharedTripSystem.Test.Controllers
{
    public class CarsControllerTests
    {
        [Fact]
        public void AllShouldReturnRedirect() =>
             MyController<CarsController>
             .Instance()
             .Calling(c => c.All())
             .ShouldReturn()
             .RedirectToAction("Create", "Drivers");
        
    }
}
