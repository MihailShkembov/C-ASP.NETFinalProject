using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SharedTripSystem.Models.Trips
{
    public class CreateTripFormModel
    {
        public string StartPoint { get; init; }
        public string EndPoint { get; init; }
        public int FreeSeats { get; init; }
        public string DestinationImageUrl { get; init; }
        public string CarPlateNumber { get; init; }
        public DateTime DepartureDate { get; init; }
    }
}
