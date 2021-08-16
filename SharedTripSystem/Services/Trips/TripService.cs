using SharedTripSystem.Data;
using SharedTripSystem.Data.Models;
using SharedTripSystem.Models.Trips;
using System;
using System.Collections.Generic;
using System.Linq;

namespace SharedTripSystem.Services.Trips
{
    public class TripService : ITripService
    {
        private readonly ApplicationDbContext dbContext;
        public TripService(ApplicationDbContext dbContext)
        {
            this.dbContext = dbContext;
        }
        public void Create(string userId, CreateTripFormModel trip)
        {
            var tripToAdd = new Trip
            {
                StartPoint = trip.StartPoint,
                EndPoint = trip.EndPoint,
                FreeSeats = trip.FreeSeats,
                DestinationImageUrl = trip.DestinationImageUrl,
                DepartureDate = trip.DepartureDate,
                Description = trip.Description,
                UserId = userId
            };
            this.dbContext.Trips.Add(tripToAdd);
            this.dbContext.SaveChanges();
        }
        public AllTripsQueryModel All(string userId,AllTripsQueryModel query)
        {
            var trips = this.dbContext.Trips.AsQueryable()
              .Where(x => DateTime.Compare(x.DepartureDate, DateTime.UtcNow) > 0 && x.FreeSeats >= 1 && !x.PassengersTrips.Any(x => x.Passenger.UserId == userId))
              .OrderBy(x => x.DepartureDate)
              .Skip((query.CurrentPage - 1) * AllTripsQueryModel.TripsPerPages)
              .Take(AllTripsQueryModel.TripsPerPages)
              .Select(x => new AllTripsViewModel
              {
                  Id = x.Id,
                  StartPoint = x.StartPoint,
                  EndPoint = x.EndPoint,
                  DepartureDate = x.DepartureDate,
                  DestinationImageUrl = x.DestinationImageUrl,
                  CarId = x.CarId,
                  FreeSeats = x.FreeSeats,
                  UserId = x.UserId
              }).ToList();
            query.Trips = trips;
            query.TotalTrips = this.dbContext.Trips.Where(x => DateTime.Compare(x.DepartureDate, DateTime.UtcNow) > 0).Count();
            return query;
        }
        public IEnumerable<AllCarsTripViewModel> AllCars(string driverId,string tripId)
        {
            var cars = this.dbContext.Cars
             .Where(x => x.DriverId == driverId)
             .Select(x => new AllCarsTripViewModel
             {

                 Id = x.Id,
                 Model = x.Model,
                 PlateNumber = x.PlateNumber,
                 KilometersTravelled = x.KilometersTravlled,
                 Type = x.Type,
                 CarImageUrl = x.CarImageUrl,
                 TripId = tripId

             }).OrderBy(x => x.KilometersTravelled)
           .ToList();
            return cars;
        }
        public void AddCar(string carId, string tripId)
        {
            var trip = this.dbContext.Trips.FirstOrDefault(x => x.Id == tripId);
            var car = this.dbContext.Cars.FirstOrDefault(x => x.Id == carId);
            trip.Car = car;
            this.dbContext.SaveChanges();
        }
        public DetailsViewModel Details(string carId, string tripId,string userId)
        {
            var trip = this.dbContext.Trips.FirstOrDefault(x => x.Id == tripId);
            var car = this.dbContext.Cars.FirstOrDefault(x => x.Id == carId);
            var details = new DetailsViewModel();
            if (car == null)
            {
                details = new DetailsViewModel
                {
                    UserId = userId,
                    TripId = trip.Id,
                    TripDescription = trip.Description
                };
            }
            else
            {
                details = new DetailsViewModel
                {
                    UserId = userId,
                    Model = car.Model,
                    Type = car.Type,
                    KilometersTravlled = car.KilometersTravlled,
                    CarImageUrl = car.CarImageUrl,
                    PlateNumber = car.PlateNumber,
                    TripId = trip.Id,
                    TripDescription = trip.Description
                };
            }
            return details;
        }

        public void Join(string userId, string tripId)
        {
            var trip = this.dbContext.Trips.FirstOrDefault(x => x.Id == tripId);
            var user = this.dbContext.Users.FirstOrDefault(x => x.Id == userId);
            trip.FreeSeats--;
            var passenger = new Passenger
            {
                UserId = user.Id,
            };
            var passengerTrip = new PassengerTrip
            {
                PassengerId = passenger.Id,
                TripId = trip.Id
            };
            trip.PassengersTrips.Add(passengerTrip);
            this.dbContext.Passengers.Add(passenger);
            this.dbContext.SaveChanges();
        }

        public IEnumerable<MyTripsListingViewModel> MyTrips(string userId)
        {
            var myTrips = this.dbContext.Trips
               .Where(x => x.UserId == userId || x.PassengersTrips.Any(pt => pt.Passenger.UserId == userId))
               .Select(x => new MyTripsListingViewModel
               {
                   StartPoint = x.StartPoint,
                   EndPoint = x.EndPoint,
                   DepartureDate = x.DepartureDate,
                   DestinationImageUrl = x.DestinationImageUrl,
                   CarId = x.CarId,
                   FreeSeats = x.FreeSeats,
               })
               .ToList();
            return myTrips;
        }
    }
}
