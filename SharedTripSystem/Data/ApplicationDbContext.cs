using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using SharedTripSystem.Data.Models;

namespace SharedTripSystem.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public virtual DbSet<Car> Cars { get; set; }
        public virtual DbSet<Trip> Trips { get; set; }
        public virtual DbSet<Driver> Drivers { get; set; }
        public virtual DbSet<Recommendation> Recommendations { get; set; }

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }
        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder
          .Entity<Car>()
          .HasOne(c => c.Driver)
          .WithMany(d => d.Cars)
          .HasForeignKey(c => c.DriverId)
          .OnDelete(DeleteBehavior.Restrict);
            builder
             .Entity<Trip>()
             .HasOne(c => c.Car)
             .WithMany(c => c.Trips)
             .HasForeignKey(c => c.CarId)
             .OnDelete(DeleteBehavior.Restrict);
            builder
                .Entity<Driver>()
                .HasOne<User>()
                .WithOne()
                .HasForeignKey<Driver>(d => d.UserId)
                .OnDelete(DeleteBehavior.Restrict);
            base.OnModelCreating(builder);

        }
    }
}
