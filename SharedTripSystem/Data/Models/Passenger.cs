using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace SharedTripSystem.Data.Models
{
    public class Passenger
    {
        public string Id { get; set; } = Guid.NewGuid().ToString();
        [Required]
        public string UserId  { get; set; }
        public User User { get; set; }
        public ICollection<PassengerTrip> PassengersTrips { get; set; } = new List<PassengerTrip>();
    }
}
