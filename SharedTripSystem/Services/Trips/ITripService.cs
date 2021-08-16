using SharedTripSystem.Models.Trips;
using System.Collections.Generic;

namespace SharedTripSystem.Services.Trips
{
    public interface ITripService
    {
        public void Create(string userId, CreateTripFormModel trip);
        public AllTripsQueryModel All(string userId,AllTripsQueryModel query);
        public IEnumerable<AllCarsTripViewModel> AllCars(string driverId,string tripId);
        public void AddCar(string carId, string tripId);
        public DetailsViewModel Details(string carId, string tripId,string userId);
        public void Join(string userId, string tripId);
        public IEnumerable<MyTripsListingViewModel> MyTrips(string userId);
    }
}
