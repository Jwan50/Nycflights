using System;

namespace Nycflights.Models
{
    public class Weather
    {
        public string? Origin { get; set; }
        public int Year { get; set; }
        public int Month { get; set; }
        public int Day { get; set; }
        public int Hour { get; set; }
        public float Temp { get; set; }
        public float Dewp { get; set; }
        public float Humid { get; set; }
        public int? Wind_dir { get; set; }
        public float? Wind_speed { get; set; }
        public double? Wind_gust { get; set; }
        public float Precip { get; set; }
        public float? Pressure { get; set; }
        public float Visib { get; set; }
        public DateTime Time_hour { get; set; }
    }
}
