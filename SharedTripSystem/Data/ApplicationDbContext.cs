using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using SharedTripSystem.Data.Models;

namespace SharedTripSystem.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public DbSet<Trip> Trips { get; set; }
        public DbSet<Car> Cars { get; set; }
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }
    }
}
