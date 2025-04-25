using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Flight.API.Controllers
{
    [Authorize(Roles = "Admin")]
    [Route("api/[controller]")]
    [ApiController]
    public class FlightController : ControllerBase
    {


        [HttpGet]
        public IActionResult GetAllFlights()
        {
            return Ok("Return all flights (admin only)");
        }

        [HttpPost]
        public IActionResult AddFlight()
        {
            return Ok("Flight added successfully");
        }

        [HttpPut("{id}")]
        public IActionResult UpdateFlight(int id)
        {
            return Ok($"Flight {id} updated successfully");
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteFlight(int id)
        {
            return Ok($"Flight {id} deleted successfully");
        }


    }
}
