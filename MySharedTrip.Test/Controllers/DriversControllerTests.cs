using Xunit;
using MyTested.AspNetCore.Mvc;
using SharedTripSystem.Controllers;
using SharedTripSystem.Models.Drivers;
using SharedTripSystem.Data.Models;
using System.Linq;

namespace SharedTripSystem.Test.Controllers
{
   public class DriversControllerTests
    {
        [Theory]
        [InlineData("Mihail Mihailov","123456")]
        public void PostShouldBeForAuthorizedUsersAndShouldRedirectWithModel
            (string fullName,string driversLicense)
        => MyController<DriversController>
            .Instance(controller=>controller
            .WithUser())
            .Calling(c=>c.Create(new BecomeDriverFormModel 
            {
                FullName=fullName,
                DriversLicense=driversLicense
            }))
            .ShouldHave()
            .ActionAttributes(attributes=>attributes
            .RestrictingForHttpMethod(HttpMethod.Post)
            .RestrictingForAuthorizedRequests())
            .ValidModelState()
            .Data(data => data
                    .WithSet<Driver>(drivers => drivers
                        .Any(d =>
                            d.FullName == fullName &&
                            d.DriversLicense == driversLicense &&
                            d.UserId == TestUser.Identifier)))
             .AndAlso()
                .ShouldReturn()
                .Redirect(redirect => redirect
                    .To<CarsController>(c => c.Create()));
          



    }
}
