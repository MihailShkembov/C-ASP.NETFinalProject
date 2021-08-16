using SharedTripSystem.Data.Models;
using SharedTripSystem.Models.Cars;
using System.Collections.Generic;

namespace SharedTripSystem.Services.Cars
{
   public interface ICarService
    {
        public void Create(string userId,AddCarFormModel car);
        public void Delete(string carId);
        public IEnumerable<AllCarsLlistingViewModel> All(Driver driver);
    }
}
