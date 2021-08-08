namespace SharedTripSystem.Models.Trips
{
    public class AllCarsTripViewModel
    {
    
            public string Id { get; set; }
            public string Model { get; set; }
            public string PlateNumber { get; set; }
            public int KilometersTravelled { get; set; }
            public string Type { get; set; }

            public string CarImageUrl { get; set; }
            public string TripId { get; set; }
        
    }

}

