using SharedTripSystem.Data;
using System.Linq;

namespace SharedTripSystem.Services.Drivers
{
    public class DriverSerice : IDriverService
    {
        private readonly ApplicationDbContext dbContext;

        public DriverSerice(ApplicationDbContext dbContext)
            => this.dbContext = dbContext;

        public bool IsDriver(string userId)
            => this.dbContext
                .Drivers
                .Any(d => d.UserId == userId);
    }
}
