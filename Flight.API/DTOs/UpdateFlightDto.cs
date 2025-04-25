namespace Flight.API.DTOs
{
    public class UpdateFlightDto
    {
        public string? FlightNumber { get; set; }
        public string? FromCity { get; set; }
        public string? ToCity { get; set; }
        public DateTime? DepartureDate { get; set; }
        public int? AvailableSeats { get; set; }
        public decimal? Price { get; set; }
    }
}
