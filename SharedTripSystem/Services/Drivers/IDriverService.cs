using SharedTripSystem.Data.Models;

namespace SharedTripSystem.Services.Drivers
{
    public interface IDriverService
    {
        public bool IsDriver(string userId);
        public void AddDriver(string userId, string fullName, string driversLicense);
        public Driver FindByUserId(string userId);
        public bool HasDriverCar(string driverId);
    }
}
