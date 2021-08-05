using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SharedTripSystem.Data;
using SharedTripSystem.Models.Cars;
using  SharedTripSystem.Data.Models;
using System.Security.Claims;
using SharedTripSystem.Services.Drivers;
using System.Linq;

namespace SharedTripSystem.Controllers
{
    public class CarsController : Controller
    {
        private readonly ApplicationDbContext dbContext;
        private readonly IDriverService drivers;
        public CarsController(ApplicationDbContext dbContext,
            IDriverService drivers)
        {
            this.dbContext = dbContext;
            this.drivers = drivers;
        }
        [Authorize]
        public IActionResult Create() => this.View();
        [HttpPost]
        [Authorize]
        public IActionResult Create(AddCarFormModel car)
        {
            if (!this.ModelState.IsValid)
            {
                return this.View(car);
            }
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (!drivers.IsDriver(userId))
            {
                return this.RedirectToAction("Create", "Drivers");
            }
            var carToAdd = new Car
            {
                PlateNumber = car.PlateNumber,
                KilometersTravlled = car.KilometersTravlled,
                Type = car.Type,
                DriverId = userId,
            };
            var driver = this.dbContext.Drivers.FirstOrDefault(x => x.UserId == userId);
            driver.Cars.Add(carToAdd);
            this.dbContext.Cars.Add(carToAdd);
            this.dbContext.SaveChanges();
            return RedirectToAction("All", "Cars");            
        }
    }
}
