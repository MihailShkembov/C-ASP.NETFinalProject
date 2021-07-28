using Microsoft.AspNetCore.Mvc;
using SharedTripSystem.Models.Trips;

namespace SharedTripSystem.Controllers
{
    public class TripController : Controller
    {
        public IActionResult Create() => View();
        [HttpPost]
        public IActionResult Create(CreateTripFormModel trip)
        {
            return View();
        }

    }
}
