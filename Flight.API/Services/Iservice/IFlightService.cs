using Flight.API.DTOs;

namespace Flight.API.Services.Iservice
{
    public interface IFlightService
    {
        Task<IEnumerable<FlightDto>> GetAllFlightsAsync();
        Task<FlightDto> GetFlightByIdAsync(int id);
        Task<IEnumerable<FlightDto>> SearchFlightsAsync(string from, string to, DateTime date);
        Task<FlightDto> AddFlightAsync(AddFlightDto flight);
        Task<bool> UpdateFlightAsync(int id, UpdateFlightDto flight);
        Task<bool> DeleteFlightAsync(int id);
    }
}
