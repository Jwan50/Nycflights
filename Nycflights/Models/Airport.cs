using System.ComponentModel.DataAnnotations;

namespace Nycflights.Models
{
    public class Airport
    {
        [Key]
        public string? Faa { get; set; }
        public string? Name { get; set; }
        public double Lat { get; set; }
        public double Lon { get; set; }
        public int Alt { get; set; }
        public int Tz { get; set; }
        public string? Dst { get; set; }
        public string? Tzone { get; set; } 
    }
}
