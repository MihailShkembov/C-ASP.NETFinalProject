using SharedTripSystem.Data;
using SharedTripSystem.Models.Cars;
using System.Linq;
using SharedTripSystem.Data.Models;
using System.Collections.Generic;

namespace SharedTripSystem.Services.Cars
{
    public class CarService : ICarService
    {
        private readonly ApplicationDbContext dbContext;
        public CarService(ApplicationDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public IEnumerable<AllCarsLlistingViewModel> All(Driver driver)
        {
            var cars = this.dbContext.Cars
              .Where(x => x.DriverId == driver.Id)
              .Select(x => new AllCarsLlistingViewModel
              {
                  Id = x.Id,
                  Model = x.Model,
                  PlateNumber = x.PlateNumber,
                  KilometersTravelled = x.KilometersTravlled,
                  Type = x.Type,
                  CarImageUrl = x.CarImageUrl
              }).OrderBy(x => x.KilometersTravelled)
            .ToList();
            return cars;
        }

        public void Create(string userId, AddCarFormModel car)
        {
            var driver = this.dbContext.Drivers.FirstOrDefault(x => x.UserId == userId);
            var carToAdd = new Car
            {
                Model = car.Model,
                PlateNumber = car.PlateNumber,
                KilometersTravlled = car.KilometersTravlled,
                Type = car.Type,
                DriverId = userId,
                CarImageUrl = car.CarImageUrl
            };
            driver.Cars.Add(carToAdd);
            this.dbContext.Cars.Add(carToAdd);
            this.dbContext.SaveChanges();
        }

        public void Delete(string carId)
        {
            var carToDelete = this.dbContext.Cars.FirstOrDefault(x => x.Id == carId);
            this.dbContext.Remove(carToDelete);
            this.dbContext.SaveChanges();
        }
    }
}
