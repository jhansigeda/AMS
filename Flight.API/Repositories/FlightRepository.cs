using Flight.API.Data;
using Flight.API.Models;
using Microsoft.EntityFrameworkCore;
using System.Reflection.Metadata.Ecma335;
using FlightModel = Flight.API.Models.Flight;

namespace Flight.API.Repositories
{
    public class FlightRepository : IFlightRepository
    {
        private readonly ApplicationDbContext _context;

        public FlightRepository(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task<IEnumerable<FlightModel>> GetAllAsync()
        {
            return await _context.Flights.ToListAsync();
        }
        public async Task<FlightModel> GetByIdAsync(int id)
        {
            return await _context.Flights.FindAsync(id);
        }

        public async Task<IEnumerable<FlightModel>> SearchFlightsAsync(string from, string to, DateTime date)
        {
           return await _context.Flights
                                .Where( f => f.FromCity == from && f.ToCity == to && f.DepartureDate.Date == date.Date)
                                .ToListAsync(); 
        }

        public async Task AddAsync(FlightModel flight)
        {
            await _context.Flights.AddAsync(flight);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(FlightModel flight)
        {
            _context.Flights.Update(flight);
            await _context.SaveChangesAsync();
        }        

      
        public async Task DeleteAsync(int id)
        {
            var flight = await _context.Flights.FindAsync(id);
            if (flight != null)
            {
                _context.Flights.Remove(flight);
                await _context.SaveChangesAsync();
            }

        }


    }
}
