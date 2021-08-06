using Microsoft.AspNetCore.Mvc;
using SharedTripSystem.Data;
using SharedTripSystem.Models.Trips;
using SharedTripSystem.Data.Models;
using System.Linq;
using System;
using Microsoft.AspNetCore.Authorization;

namespace SharedTripSystem.Controllers
{
    public class TripsController : Controller
    {
        private readonly ApplicationDbContext dbContext;
        public TripsController(ApplicationDbContext dbContext)
        {
            this.dbContext = dbContext;
        }
        [Authorize]
        public IActionResult Create()
        {
            
            return this.View();
        }

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
                    FreeSeats = x.FreeSeats
                }).ToList();
            query.Trips = trips;
            query.TotalTrips = this.dbContext.Trips.Where(x => DateTime.Compare(x.DepartureDate, DateTime.UtcNow) > 0).Count();
            return this.View(query);
        }

    }
}
