using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SharedTripSystem.Data;
using SharedTripSystem.Data.Models;
using SharedTripSystem.Models.Drivers;
using SharedTripSystem.Services.Drivers;
using System.Security.Claims;


namespace SharedTripSystem.Controllers
{
    public class DriversController:Controller
    {

        private readonly ApplicationDbContext dbContext;
        private readonly IDriverService drivers;
        public DriversController(ApplicationDbContext dbContext,
            IDriverService drivers)
        {
            this.dbContext = dbContext;
            this.drivers = drivers;
        }
        [Authorize]
        public IActionResult Create() => this.View();
        [Authorize]
        [HttpPost]
        public IActionResult Create(BecomeDriverFormModel driver)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var isUserDriver = drivers.IsDriver(userId);
            if (isUserDriver)
            {
                return BadRequest();
            }
            if (!ModelState.IsValid)
            {
                return this.View(driver);
            }
            this.drivers.AddDriver(userId, driver.FullName, driver.DriversLicense);
            return this.RedirectToAction("Create", "Cars");
        }

    }
}
