using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SharedTripSystem.Models.Trips
{
    public class AllTripsQueryModel
    {
        public const int TripsPerPages = 2;
        public int CurrentPage { get; set; } = 1;

        public List<AllTripsViewModel> Trips { get; set; } = new List<AllTripsViewModel>();
        public int TotalTrips { get; set; }
    }
}
