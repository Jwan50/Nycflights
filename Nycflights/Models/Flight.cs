using System;

namespace Nycflights.Models
{
    public class Flight
    {
        public int Year { get; set; }
        public int Month { get; set; }
        public int Day { get; set; }
        public TimeSpan? Dep_time { get; set; }
        public int? Dep_delay { get; set; }
        public TimeSpan? Arr_time { get; set; }
        public int? Arr_delay { get; set; }
        public string? Carrier { get; set; }
        public string? Tailnum { get; set; }
        public int FlightNumber { get; set; }
        public string? Origin { get; set; }
        public string? Dest { get; set; }
        public int? Air_time { get; set; }
        public int Distance { get; set; }
        public int? Hour { get; set; }
        public int? Minute { get; set; }
    }
}
