
namespace SharedTripSystem.Infrastructure
{
using CarRentingSystem.Data;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
    public static class ApplicationBuilderExtension
    {
        public static IApplicationBuilder PrepareDatabase
            (this IApplicationBuilder app)
        {
           using var scopedServices = app.ApplicationServices.CreateScope();
            var data = scopedServices.ServiceProvider.GetService<ApplicationDbContext>();
            data.Database.Migrate();
            return app;
        }
    }
}
