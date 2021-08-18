using System.Collections.Generic;
using System.Linq;
using  SharedTripSystem.Data.Models;

namespace SharedTripSystem.Test.Data
{
    public static class Cars
    {
        public static IEnumerable<Car> TenPublicCars
            => Enumerable.Range(0, 10).Select(i => new Car { });
        public static string CarId = "9114bdbc-ada7-48c4-984d-8a94d1141da3";


    }
}
