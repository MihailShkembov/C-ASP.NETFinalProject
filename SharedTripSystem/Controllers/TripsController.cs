using Microsoft.AspNetCore.Mvc;
using SharedTripSystem.Data;
using SharedTripSystem.Models.Trips;
using SharedTripSystem.Data.Models;
using System.Linq;
using System;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;
using SharedTripSystem.Services.Drivers;

namespace SharedTripSystem.Controllers
{
    public class TripsController : Controller
    {
        private readonly ApplicationDbContext dbContext;
        private readonly IDriverService drivers;
        public TripsController(ApplicationDbContext dbContext, IDriverService drivers)
        {
            this.dbContext = dbContext;
            this.drivers = drivers;
        }
        [Authorize]
        public IActionResult Create()
        {
            
            return this.View();
        }
        [Authorize]
        [HttpPost]
        public IActionResult Create(CreateTripFormModel trip)
        {
            if (!ModelState.IsValid)
            {
                return this.View(trip);
            }
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (!drivers.IsDriver(userId))
            {
                return RedirectToAction("Create", "Drivers");
            }
            var tripToAdd = new Trip
            {
                StartPoint = trip.StartPoint,
                EndPoint = trip.EndPoint,
                FreeSeats = trip.FreeSeats,
                DestinationImageUrl = trip.DestinationImageUrl,
                DepartureDate = trip.DepartureDate,
                Description = trip.Description,
                UserId=userId
        };
            this.dbContext.Trips.Add(tripToAdd);
            this.dbContext.SaveChanges();
            return this.RedirectToAction("Index", "Home");
        }
        [Authorize]
        public IActionResult All([FromQuery]AllTripsQueryModel query)
        { 
           
            var trips = this.dbContext.Trips.AsQueryable()
                .Where(x => DateTime.Compare(x.DepartureDate, DateTime.UtcNow) > 0)
                .OrderBy(x => x.DepartureDate)
                .Skip((query.CurrentPage-1)*AllTripsQueryModel.TripsPerPages)
                .Take(AllTripsQueryModel.TripsPerPages)
                .Select(x => new AllTripsViewModel
                {
                    Id = x.Id,
                    StartPoint = x.StartPoint,
                    EndPoint = x.EndPoint,
                    DepartureDate = x.DepartureDate,
                    DestinationImageUrl = x.DestinationImageUrl,
                    CarId=x.CarId,
                    FreeSeats = x.FreeSeats,
                    UserId=x.UserId
                }).ToList();
            query.Trips = trips;
            query.TotalTrips = this.dbContext.Trips.Where(x => DateTime.Compare(x.DepartureDate, DateTime.UtcNow) > 0).Count();
            return this.View(query);
        }
        public IActionResult AllCars(string tripId)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (!drivers.IsDriver(userId))
            {
                return this.RedirectToAction("Create", "Drivers");
            }
            var driver = this.dbContext.Drivers.FirstOrDefault(x => x.UserId == userId);
            if (!this.dbContext.Cars.Any(x => x.DriverId == driver.Id))
            {
                return this.RedirectToAction("Create", "Cars");
            }
            var cars = this.dbContext.Cars
               .Where(x => x.DriverId == driver.Id)
               .Select(x => new AllCarsTripViewModel
               {

                   Id = x.Id,
                   Model = x.Model,
                   PlateNumber = x.PlateNumber,
                   KilometersTravelled = x.KilometersTravlled,
                   Type = x.Type,
                   CarImageUrl = x.CarImageUrl,
                   TripId=tripId

               }).OrderBy(x => x.KilometersTravelled)
             .ToList();

            return this.View(cars);
        }
        public IActionResult AddCar(string carId,string tripId)
        {
            var trip = this.dbContext.Trips.FirstOrDefault(x => x.Id == tripId);
            var car = this.dbContext.Cars.FirstOrDefault(x => x.Id == carId);
            trip.Car = car;
            this.dbContext.SaveChanges();
            return RedirectToAction("All", "Trips");
        }
        public IActionResult Details(string carId, string tripId)
        {
            var trip = this.dbContext.Trips.FirstOrDefault(x => x.Id == tripId);
            var car = this.dbContext.Cars.FirstOrDefault(x => x.Id == carId);
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var details = new DetailsViewModel
            {
                UserId=userId,
                Model = car.Model,
                Type = car.Type,
                KilometersTravlled = car.KilometersTravlled,
                CarImageUrl = car.CarImageUrl,
                PlateNumber = car.PlateNumber,
                TripId=trip.Id,
                TripDescription = trip.Description
            };
            return this.View(details);
        }
        //public IActionResult Join(string userId, string tripId)
        //{
        //    return this.View();
        //}
    }
}
