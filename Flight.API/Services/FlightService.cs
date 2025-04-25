
using Flight.API.DTOs;
using Flight.API.Repositories;
using Flight.API.Services.Iservice;
using FlightModel = Flight.API.Models.Flight;
using Microsoft.Identity.Client;
using AutoMapper;

namespace Flight.API.Services
{
    public class FlightService : IFlightService
    {
        private readonly IFlightRepository _flightRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public FlightService(IFlightRepository flightRepository, IUnitOfWork unitOfWork,  IMapper mapper)
        {
            _flightRepository = flightRepository;
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<IEnumerable<FlightDto>> GetAllFlightsAsync()
        {
            var flights = await _flightRepository.GetAllAsync();
            return flights.Select(f => new FlightDto
            {    
                Id = f.Id,
                FlightNumber = f.FlightNumber,
                FromCity = f.FromCity,
                ToCity = f.ToCity,
                DepartureDate = f.DepartureDate,
                AvailableSeats = f.AvailableSeats,
                Price = f.Price
            });
        }

        public async Task<FlightDto> GetFlightByIdAsync(int id)
        {
            var flight = await _flightRepository.GetByIdAsync(id);
            if (flight == null)
            {
                return null;
            }
            return new FlightDto
            {
                Id= flight.Id,
                FlightNumber = flight.FlightNumber,
                FromCity = flight.FromCity,
                ToCity = flight.ToCity,
                DepartureDate = flight.DepartureDate,
                AvailableSeats = flight.AvailableSeats,
                Price = flight.Price
            };
        }

        public async Task<IEnumerable<FlightDto>> SearchFlightsAsync(string from, string to, DateTime date)
        {
            var flights = await _flightRepository.SearchFlightsAsync(from, to, date);
            return flights.Select(f => new FlightDto
            {   
                Id = f.Id,
                FlightNumber = f.FlightNumber,
                FromCity = f.FromCity,
                ToCity = f.ToCity,
                DepartureDate = f.DepartureDate,
                AvailableSeats = f.AvailableSeats,
                Price = f.Price
            });
        }

        public async Task<FlightDto> AddFlightAsync(AddFlightDto addflightDto)
        {
            var flightEntity = _mapper.Map<FlightModel>(addflightDto);

            await _flightRepository.AddAsync(flightEntity);
            await _unitOfWork.SaveAsync();
            return _mapper.Map<FlightDto>(flightEntity); // Return back the saved object with ID
        }

        public async Task<bool> UpdateFlightAsync(int id, UpdateFlightDto dto)
        {
            var existingFlight = await _flightRepository.GetByIdAsync(id);
            if (existingFlight == null)
                return false;

            // Only update properties that are provided
            if (!string.IsNullOrWhiteSpace(dto.FlightNumber))
                existingFlight.FlightNumber = dto.FlightNumber;

            if (!string.IsNullOrWhiteSpace(dto.FromCity))
                existingFlight.FromCity = dto.FromCity;

            if (!string.IsNullOrWhiteSpace(dto.ToCity))
                existingFlight.ToCity = dto.ToCity;

            if (dto.DepartureDate.HasValue)
                existingFlight.DepartureDate = dto.DepartureDate.Value;

            if (dto.AvailableSeats.HasValue)
                existingFlight.AvailableSeats = dto.AvailableSeats.Value;

            if (dto.Price.HasValue)
                existingFlight.Price = dto.Price.Value;

            await _flightRepository.UpdateAsync(existingFlight);
            await _unitOfWork.SaveAsync(); // Ensure changes are persisted
            return true;
           
        }

        public async Task<bool> DeleteFlightAsync(int id)
        {
            var flight = await _flightRepository.GetByIdAsync(id);
            if (flight == null)
                return false;

            await _flightRepository.DeleteAsync(id);
            await _unitOfWork.SaveAsync();
            return true;
        }

    }
}
