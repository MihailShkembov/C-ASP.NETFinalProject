namespace SharedTripSystem.Models.Trips
{
    public class DetailsViewModel
    {
        public string UserId { get; set; }
        public string TripId { get; set; }
        public string Model { get; set; }
        public string PlateNumber { get; set; }
        public int KilometersTravlled { get; set; }
        public string Type { get; set; }
        public string CarImageUrl { get; set; }
        public string TripDescription { get; set; }
    }
}
