using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SharedTripSystem.Data;
using SharedTripSystem.Models.Cars;
using SharedTripSystem.Data.Models;
using System.Security.Claims;
using SharedTripSystem.Services.Drivers;
using System.Linq;
using SharedTripSystem.Services.Cars;

namespace SharedTripSystem.Controllers
{
    public class CarsController : Controller
    {
        private readonly ApplicationDbContext dbContext;
        private readonly IDriverService drivers;
        private readonly ICarService cars;
        public CarsController(ApplicationDbContext dbContext,
            IDriverService drivers,
            ICarService cars)
        {
            this.dbContext = dbContext;
            this.drivers = drivers;
            this.cars = cars;
        }
        [Authorize]
        public IActionResult All()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (!drivers.IsDriver(userId))
            {
                return this.RedirectToAction("Create", "Drivers");
            }
            var driver = this.drivers.FindByUserId(userId);
            if (!this.dbContext.Cars.Any(x=>x.DriverId==driver.Id))
            {
                return this.RedirectToAction("Create", "Cars");
            }
            var cars = this.cars.All(driver);
            return this.View(cars);
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
            this.cars.Create(userId,car);
            return RedirectToAction("All", "Cars");
        }
        [Authorize]
        public IActionResult Delete(string carId)
        {
            this.cars.Delete(carId);
            return RedirectToAction("Index", "Home");
        }
    }
}
