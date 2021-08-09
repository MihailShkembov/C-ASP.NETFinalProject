using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SharedTripSystem.Data.Models
{
    public class PassengerTrip
    {
        [Required]
        [ForeignKey("Trip")]
        public string TripId { get; set; }
        public Trip Trip { get; set; }
        [Required]
        [ForeignKey("Passenger")]
        public string PassengerId { get; set; }
        public Passenger Passenger { get; set; }
    }
}
