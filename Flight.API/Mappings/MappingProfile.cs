using AutoMapper;
using Flight.API.Models;
using Flight.API.DTOs;

namespace Flight.API.Mappings
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Flight.API.Models.Flight, FlightDto>().ReverseMap(); // Bi-directional mapping
            CreateMap<Flight.API.Models.Flight, AddFlightDto>().ReverseMap(); // Bi-directional mapping
            CreateMap<Flight.API.Models.Flight, UpdateFlightDto>().ReverseMap(); // Bi-directional mapping
        }
    }
}
