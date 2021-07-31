using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SharedTripSystem.Data;
using SharedTripSystem.Models.Trips;
using SharedTripSystem.Data.Models;

namespace SharedTripSystem.Controllers
{
    public class TripsController : Controller
    {
        private readonly ApplicationDbContext dbContext;
        public TripsController(ApplicationDbContext dbContext)
        {
            this.dbContext = dbContext;
        }
        public IActionResult Create() => this.View();
        [HttpPost]
        public IActionResult Create(CreateTripFormModel trip)
        {
            if (!ModelState.IsValid)
            {
                return this.View(trip);
            }
         var tripToAdd = new Trip
            {
                StartPoint = trip.StartPoint,
                EndPoint = trip.EndPoint,
                FreeSeats = trip.FreeSeats,
                DestinationImageUrl = trip.DestinationImageUrl,
                DepartureDate = trip.DepartureDate,
                Description = trip.Description
            };
            this.dbContext.Trips.Add(tripToAdd);
            this.dbContext.SaveChanges();
            return this.RedirectToAction("Index", "Home");
        }

    }
}
