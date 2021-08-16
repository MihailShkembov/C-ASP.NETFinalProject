using Microsoft.AspNetCore.Mvc;
using SharedTripSystem.Data;
using SharedTripSystem.Models.Trips;
using SharedTripSystem.Data.Models;
using System.Linq;
using System;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;
using SharedTripSystem.Services.Drivers;
using SharedTripSystem.Services.Trips;

namespace SharedTripSystem.Controllers
{
    public class TripsController : Controller
    {
        private readonly ApplicationDbContext dbContext;
        private readonly IDriverService drivers;
        private readonly ITripService trips;
        public TripsController(ApplicationDbContext dbContext
            ,IDriverService drivers
            ,ITripService trips)
        {
            this.dbContext = dbContext;
            this.drivers = drivers;
            this.trips = trips;
        }
        [Authorize]
        public IActionResult Create()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (!drivers.IsDriver(userId))
            {
                return RedirectToAction("Create", "Drivers");
            } 
            return this.View();
        }
        [Authorize]
        [HttpPost]
        public IActionResult Create(CreateTripFormModel trip)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (!ModelState.IsValid)
            {
                return this.View(trip);
            }
            this.trips.Create(userId, trip);
            return this.RedirectToAction("AllCars", "Trips");
        }
        [Authorize]
        public IActionResult All([FromQuery]AllTripsQueryModel query)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var viewModel = this.trips.All(userId, query);
            return this.View(viewModel);
        }
        [Authorize]
        public IActionResult AllCars(string tripId)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (!drivers.IsDriver(userId))
            {
                return this.RedirectToAction("Create", "Drivers");
            }
            var driver = drivers.FindByUserId(userId);
            if (!this.drivers.HasDriverCar(driver.Id))
            {
                return this.RedirectToAction("Create", "Cars");
            }
            var cars = this.trips.AllCars(driver.Id, tripId);
            return this.View(cars);
        }
        [Authorize]
        public IActionResult AddCar(string carId,string tripId)
        {
            this.trips.AddCar(carId, tripId);
            return RedirectToAction("All", "Trips");
        }
        [Authorize]
        public IActionResult Details(string carId, string tripId)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var details = trips.Details(carId, tripId, userId);
            return this.View(details);
        }
        [Authorize]
        public IActionResult Join(string userId, string tripId)
        {
            this.trips.Join(userId, tripId);
            return RedirectToAction("Index", "Home");
        }
        public IActionResult MyTrips()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var myTrips = trips.MyTrips(userId);
            if (!myTrips.Any())
            {
                return RedirectToAction("All","Trips");
            }
            return this.View(myTrips);
        }

    }
}
