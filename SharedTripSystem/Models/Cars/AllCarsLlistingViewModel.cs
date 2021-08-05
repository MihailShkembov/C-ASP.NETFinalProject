namespace SharedTripSystem.Models.Cars
{
    public class AllCarsLlistingViewModel
    {
        public string Id { get; set; }
        public string Model { get; set; }
        public string PlateNumber { get; set; }
        public int KilometersTravelled { get; set; }
        public string Type { get; set; }

        public string CarImageUrl { get; set; }
    }
}
