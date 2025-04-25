using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Booking.API.Controllers
{
    [Authorize(Roles = "Customer")]
    [Route("api/[controller]")]
    [ApiController]
    public class BookingController : ControllerBase
    {
        [HttpPost("search")]
        public IActionResult SearchFlights()
        {
            return Ok("Search results based on From, To, Date, etc.");
        }

        [HttpPost("book")]
        public IActionResult BookFlight()
        {
            return Ok("Flight booked successfully");
        }

        [HttpGet("mybookings")]
        public IActionResult GetMyBookings()
        {
            return Ok("List of bookings for logged in user");
        }

        [HttpPut("change/{bookingId}")]
        public IActionResult ChangeBookingDate(int bookingId)
        {
            return Ok($"Booking {bookingId} date changed");
        }

        [HttpDelete("cancel/{bookingId}")]
        public IActionResult CancelBooking(int bookingId)
        {
            return Ok($"Booking {bookingId} cancelled");
        }

    }
}
