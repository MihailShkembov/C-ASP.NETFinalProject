using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SharedTripSystem.Data;
using SharedTripSystem.Models.Cars;
using SharedTripSystem.Data.Models;
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
        public IActionResult All()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (!drivers.IsDriver(userId))
            {
                return this.RedirectToAction("Create", "Drivers");
            }
            var driver = this.dbContext.Drivers.FirstOrDefault(x => x.UserId == userId);
            if (!this.dbContext.Cars.Any(x=>x.DriverId==driver.Id))
            {
                return this.RedirectToAction("Create", "Cars");
            }
            var cars = this.dbContext.Cars
               .Where(x => x.DriverId==driver.Id)
               .Select(x => new AllCarsLlistingViewModel
               {
                   Id = x.Id,
                   Model = x.Model,
                   PlateNumber = x.PlateNumber,
                   KilometersTravelled = x.KilometersTravlled,
                   Type = x.Type,
                   CarImageUrl = x.CarImageUrl
               }).OrderBy(x => x.KilometersTravelled)
             .ToList();

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
            var driver = this.dbContext.Drivers.FirstOrDefault(x => x.UserId == userId);
            var carToAdd = new Car
            {
                Model = car.Model,
                PlateNumber = car.PlateNumber,
                KilometersTravlled = car.KilometersTravlled,
                Type = car.Type,
                DriverId = userId,
                CarImageUrl=car.CarImageUrl
            };
            driver.Cars.Add(carToAdd);
            this.dbContext.Cars.Add(carToAdd);
            this.dbContext.SaveChanges();
            return RedirectToAction("All", "Cars");
        }
        public IActionResult Delete(string carId)
        {
            var carToDelete= this.dbContext.Cars.FirstOrDefault(x => x.Id == carId);
            this.dbContext.Remove(carToDelete);
            this.dbContext.SaveChanges();
            return RedirectToAction("Index", "Home");
        }
    }
}
