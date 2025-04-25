using FlightModel = Flight.API.Models.Flight;
namespace Flight.API.Repositories
{
    public interface IFlightRepository
    {
        Task<IEnumerable<FlightModel>> GetAllAsync();
        Task<FlightModel> GetByIdAsync(int id);
        Task<IEnumerable<FlightModel>> SearchFlightsAsync(string from, string to, DateTime date);
        Task AddAsync(FlightModel flight);
        Task UpdateAsync(FlightModel flight);
        Task DeleteAsync(int id);

    }
}
