using System.Linq;
using SharedTripSystem.Data;
using SharedTripSystem.Data.Models;

namespace SharedTripSystem.Services.Drivers
{
    public class DriverSerice : IDriverService
    {
        private readonly ApplicationDbContext dbContext;

        public DriverSerice(ApplicationDbContext dbContext)
            => this.dbContext = dbContext;

        public void AddDriver(string userId, string fullName, string driversLicense)
        {
            var driverToAdd = new Driver
            {
                UserId = userId,
                FullName = fullName,
                DriversLicense = driversLicense,
            };
            this.dbContext.Drivers.Add(driverToAdd);
            this.dbContext.SaveChanges();
        }

        public Driver FindByUserId(string userId)
        {
            return this.dbContext.Drivers.FirstOrDefault(x => x.UserId == userId);
        }

        public bool IsDriver(string userId)
            => this.dbContext
                .Drivers
                .Any(d => d.UserId == userId);

        public bool HasDriverCar(string driverId)
        {
            return this.dbContext.Cars.Any(x => x.DriverId ==driverId);
        }
    }

}
