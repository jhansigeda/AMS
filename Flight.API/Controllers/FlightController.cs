using Flight.API.DTOs;
using Flight.API.Services.Iservice;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using FlightModel = Flight.API.Models.Flight;

namespace Flight.API.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class FlightController : ControllerBase
    {
        private readonly IFlightService _flightService;
        public FlightController(IFlightService flightService)
        {
            _flightService = flightService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<FlightModel>>> GetFlights()
        {
            var flights = await _flightService.GetAllFlightsAsync();
            return Ok(flights);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<FlightModel>> GetFlightById(int id)
        {
            var flight = await _flightService.GetFlightByIdAsync(id);
            if (flight == null)
                return NotFound();
            return Ok(flight);
        }

        [HttpGet("search")]
        [AllowAnonymous]
        public async Task<ActionResult<IEnumerable<FlightModel>>> SearchFlights(string fromCity, string toCity, DateTime date)
        {
            var results = await _flightService.SearchFlightsAsync(fromCity, toCity, date);
            return Ok(results);
        }

        // POST: api/Flight
        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult<FlightDto>> AddFlight([FromBody] AddFlightDto addflighdto)
        {
            var result = await _flightService.AddFlightAsync(addflighdto);
            return CreatedAtAction(nameof(GetFlightById), new { id = result.Id}, result);
        }

        [HttpPut("{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> UpdateFlight(int id, [FromBody] UpdateFlightDto updateflightDto)
        {
            //if (id != updateflightDto.Id)
            //    return BadRequest();

            var result = await _flightService.UpdateFlightAsync(id, updateflightDto);
            if (!result)
                return NotFound();

            return NoContent();
        }

        [HttpDelete("{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> DeleteFlight(int id)
        {
            var result = await _flightService.DeleteFlightAsync(id);
            if (!result)
                return NotFound();

            return NoContent();
        }



    }
}
