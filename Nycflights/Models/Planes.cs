using System.ComponentModel.DataAnnotations;

namespace Nycflights.Models
{
    public class Planes
    {
        [Key]
        public string? Tailnum { get; set; }
        public int Year { get; set; }
        public string? Type { get; set; }
        public string? Manufacturer { get; set; }
        public string? Model { get; set; }
        public int Engines { get; set; }
        public int Seats { get; set; }
        public int? Speed { get; set; }
        public string? Engine { get; set; }
    }
}
