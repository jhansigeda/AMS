using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Flight.API.Models
{
    public class Flight
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public string FlightNumber { get; set; }
        public string FromCity { get; set; }
        public string ToCity { get; set; }
        public DateTime DepartureDate { get; set; }
        public int AvailableSeats { get; set; }
        public decimal Price { get; set; }
    }
}
