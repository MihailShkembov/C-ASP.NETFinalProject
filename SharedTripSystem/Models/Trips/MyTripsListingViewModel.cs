using System;

namespace SharedTripSystem.Models.Trips
{
    public class MyTripsListingViewModel
    {
        public string Id { get; init; }
        public string StartPoint { get; init; }
        public string EndPoint { get; init; }
        public int FreeSeats { get; init; }
        public string CarId { get; set; }
        public string DestinationImageUrl { get; init; }
        public DateTime DepartureDate { get; init; }
    }
}
