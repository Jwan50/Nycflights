using Microsoft.AspNetCore.Mvc;
using Nycflights.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Nycflights.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class NycflightsController : Controller
    {
        private Dictionary<int, string> monthsByNumber = new Dictionary<int, string>()
            { {1, "Jan" }, {2, "Feb"}, {3, "Mar"},  {4, "Apr"},  {5, "May"},  {6, "Jun"},
                {7, "Jul"},  {8, "Aug"},  {9, "Sep"},  {10, "Oct"},  {11, "Nov"},  {12, "Dec"}};

        #region Flights per month
        //1. GET: api/Nycflights/monthlyFlight
        [HttpGet("[action]")]
        public Dictionary<string, int> monthlyFlight()
        {
            var context = new FlightDBContext();

            return context.Flights.Select(f => f.Month).ToList().GroupBy(m => m).OrderBy(g => g.Key).ToDictionary(g => monthsByNumber[g.Key], g => g.Count());
        }

        //2.1. GET: api/Nycflights/JFK_MonthlyFlights
        [HttpGet("[action]")]
        public Dictionary<string, int> JFK_MonthlyFlights()
        {
            var context = new FlightDBContext();

            return context.Flights.Where(f => !string.IsNullOrEmpty(f.Origin) && f.Origin.Equals("JFK")).Select(f => f.Month).ToList()
                .GroupBy(m => m).OrderBy(g => g.Key).ToDictionary(g => monthsByNumber[g.Key], g => g.Count());
        }

        //2.2. GET: api/Nycflights/EWR_MonthlyFlights
        [HttpGet("[action]")]
        public Dictionary<string, int> EWR_MonthlyFlights()
        {
            var context = new FlightDBContext();

            return context.Flights.Where(f => !string.IsNullOrEmpty(f.Origin) && f.Origin.Equals("EWR")).Select(f => f.Month).ToList()
                .GroupBy(m => m).OrderBy(g => g.Key).ToDictionary(g => monthsByNumber[g.Key], g => g.Count());
        }

        //2.3. GET: api/Nycflights/LGA_MonthlyFligthts
        [HttpGet("[action]")]
        public Dictionary<string, int> LGA_MonthlyFligthts()
        {
            var context = new FlightDBContext();

            return context.Flights.Where(f => !string.IsNullOrEmpty(f.Origin) && f.Origin.Equals("LGA")).Select(f => f.Month).ToList()
                .GroupBy(m => m).OrderBy(g => g.Key).ToDictionary(g => monthsByNumber[g.Key], g => g.Count());
        }
        #endregion

        #region Top ten destinations
        //3.1. GET: api/Nycflights/TopTenDestFlights
        [HttpGet("[action]")]
        public Dictionary<string?, int> TopTenDestFlights()
        {
            var context = new FlightDBContext();

            return context.Flights.Select(f => f.Dest).ToList().GroupBy(d => d).ToDictionary(g => g.Key, g => g.Count())
                .OrderByDescending(val => val.Value).Take(10).ToDictionary(g => g.Key, g => g.Value);
        }

        //3.2. GET: api/Nycflights/TopTenDestFlights_from_JFK
        [HttpGet("[action]")]
        public Dictionary<string, int> TopTenDestFlights_from_JFK()
        {
            var context = new FlightDBContext();

            List<string?> topTenDestinations = context.Flights.Select(f => f.Dest).ToList().GroupBy(d => d).ToDictionary(g => g.Key, g => g.Count())
                .OrderByDescending(val => val.Value).Take(10).ToDictionary(g => g.Key, g => g.Value).Keys.ToList();

            Dictionary<string, int> flightsToTopTenDestinationsFromJFK = new Dictionary<string, int>();
            foreach (string? dest in topTenDestinations)
            {
                flightsToTopTenDestinationsFromJFK.Add(dest, context.Flights.Where(f => !string.IsNullOrEmpty(f.Origin) && f.Origin.Equals("JFK") &&
                    !string.IsNullOrEmpty(f.Dest) && f.Dest.Equals(dest)).Count());
            }

            return flightsToTopTenDestinationsFromJFK;
        }

        //3.3. GET: api/Nycflights/TopTenDestFlights_from_EWR
        [HttpGet("[action]")]
        public Dictionary<string, int> TopTenDestFlights_from_EWR()
        {
            var context = new FlightDBContext();

            List<string?> topTenDestinations = context.Flights.Select(f => f.Dest).ToList().GroupBy(d => d).ToDictionary(g => g.Key, g => g.Count())
                .OrderByDescending(val => val.Value).Take(10).ToDictionary(g => g.Key, g => g.Value).Keys.ToList();

            Dictionary<string, int> flightsToTopTenDestinationsFromEWR = new Dictionary<string, int>();
            foreach (string? dest in topTenDestinations)
            {
                flightsToTopTenDestinationsFromEWR.Add(dest, context.Flights.Where(f => !string.IsNullOrEmpty(f.Origin) && f.Origin.Equals("EWR") &&
                    !string.IsNullOrEmpty(f.Dest) && f.Dest.Equals(dest)).Count());
            }

            return flightsToTopTenDestinationsFromEWR;
        }

        //3.4. GET: api/Nycflights/TopTenDestFlights_from_LGA
        [HttpGet("[action]")]
        public Dictionary<string, int> TopTenDestFlights_from_LGA()
        {
            var context = new FlightDBContext();

            List<string?> topTenDestinations = context.Flights.Select(f => f.Dest).ToList().GroupBy(d => d).ToDictionary(g => g.Key, g => g.Count())
                .OrderByDescending(val => val.Value).Take(10).ToDictionary(g => g.Key, g => g.Value).Keys.ToList();

            Dictionary<string, int> flightsToTopTenDestinationsFromLGA = new Dictionary<string, int>();
            foreach (string? dest in topTenDestinations)
            {
                flightsToTopTenDestinationsFromLGA.Add(dest, context.Flights.Where(f => !string.IsNullOrEmpty(f.Origin) && f.Origin.Equals("LGA") &&
                    !string.IsNullOrEmpty(f.Dest) && f.Dest.Equals(dest)).Count());
            }

            return flightsToTopTenDestinationsFromLGA;
        }
        #endregion

        //4. GET: api/Nycflights/Origins_MeanAirtime
        [HttpGet("[action]")]
        public Dictionary<string, string> Origins_MeanAirtime()
        {
            var context = new FlightDBContext();

            double? averageAirtimeJFK = context.Flights.Where(f => !string.IsNullOrEmpty(f.Origin) && f.Origin.Equals("JFK")).Select(f => f.Air_time).ToList().Average();
            double? averageAirtimeEWR = context.Flights.Where(f => !string.IsNullOrEmpty(f.Origin) && f.Origin.Equals("EWR")).Select(f => f.Air_time).ToList().Average();
            double? averageAirtimeLGA = context.Flights.Where(f => !string.IsNullOrEmpty(f.Origin) && f.Origin.Equals("LGA")).Select(f => f.Air_time).ToList().Average();

            TimeSpan tavgAirtimeJFK = new TimeSpan(), tavgAirtimeEWR = new TimeSpan(), tavgAirtimeLGA = new TimeSpan();

            if (averageAirtimeJFK != null)
                tavgAirtimeJFK = TimeSpan.FromMinutes((double)averageAirtimeJFK);

            if (averageAirtimeEWR != null)
                tavgAirtimeEWR = TimeSpan.FromMinutes((double)averageAirtimeEWR);

            if (averageAirtimeLGA != null)
                tavgAirtimeLGA = TimeSpan.FromMinutes((double)averageAirtimeLGA);

            return new Dictionary<string, string>() { { "JFK", tavgAirtimeJFK.ToString("hh\\:mm\\:ss") },
                { "EWR", tavgAirtimeEWR.ToString("hh\\:mm\\:ss") },
                { "LGA", tavgAirtimeLGA.ToString("hh\\:mm\\:ss") } };
        }

        //5. GET: api/Nycflights/Weather_Obs_forOrigins
        [HttpGet("[action]")]
        public Dictionary<string?, int> Weather_Obs_forOrigins()
        {
            var context = new FlightDBContext();

            return context.Weather.Select(w => w.Origin).ToList().GroupBy(o => o).ToDictionary(g => g.Key, g => g.Count());
        }

        //6.1. GET: api/Nycflights/EWR_Celsius_temp
        [HttpGet("[action]")]
        public Dictionary<DateTime, float> EWR_Celsius_temp()
        {
            var context = new FlightDBContext();

            return context.Weather.Where(w => !string.IsNullOrEmpty(w.Origin) && w.Origin.Equals("EWR") && w.Temp >= 0)
                .Select(w => new { w.Time_hour, w.Temp }).ToDictionary(g => g.Time_hour, g => (g.Temp - 32) * 5 / 9);
        }

        //6.2. GET: api/Nycflights/LGA_Celsius_temp
        [HttpGet("[action]")]
        public Dictionary<DateTime, float> LGA_Celsius_temp()
        {
            var context = new FlightDBContext();

            return context.Weather.Where(w => !string.IsNullOrEmpty(w.Origin) && w.Origin.Equals("LGA"))
                .Select(w => new { w.Time_hour, w.Temp }).ToDictionary(g => g.Time_hour, g => (g.Temp - 32) * 5 / 9);
        }

        //7. GET: api/Nycflights/JFK_Celsius_temp
        [HttpGet("[action]")]
        public Dictionary<DateTime, float> JFK_Celsius_temp()
        {
            var context = new FlightDBContext();

            return context.Weather.Where(w => !string.IsNullOrEmpty(w.Origin) && w.Origin.Equals("JFK"))
                .Select(w => new { w.Time_hour, w.Temp }).ToDictionary(g => g.Time_hour, g => (g.Temp - 32) * 5 / 9);
        }

        //8. GET: api/Nycflights/JFK_DailyMean_CelTemp
        [HttpGet("[action]")]
        public Dictionary<string, float> JFK_DailyMean_CelTemp()
        {
            var context = new FlightDBContext();

            return context.Weather.Where(w => !string.IsNullOrEmpty(w.Origin) && w.Origin.Equals("JFK"))
                .GroupBy(g => g.Time_hour.Date.ToShortDateString()).ToDictionary(p => p.Key, p => (p.Average(g => g.Temp) - 32) * 5 / 9);
        }

        //9.1. GET: api/Nycflights/EWR_DailyMean_CelTemp
        [HttpGet("[action]")]
        public Dictionary<string, float> EWR_DailyMean_CelTemp()
        {
            var context = new FlightDBContext();

            return context.Weather.Where(w => !string.IsNullOrEmpty(w.Origin) && w.Origin.Equals("EWR") && w.Temp != null)
                .GroupBy(g => g.Time_hour.Date.ToShortDateString()).ToDictionary(p => p.Key, p => (p.Average(g => g.Temp) - 32) * 5 / 9);
        }

        //9.2. GET: api/Nycflights/LGA_DailyMean_CelTemp
        [HttpGet("[action]")]
        public Dictionary<string, float> LGA_DailyMean_CelTemp()
        {
            var context = new FlightDBContext();

            return context.Weather.Where(w => !string.IsNullOrEmpty(w.Origin) && w.Origin.Equals("LGA"))
                .GroupBy(g => g.Time_hour.Date.ToShortDateString()).ToDictionary(p => p.Key, p => (p.Average(g => g.Temp) - 32) * 5 / 9);
        }

        //10.1. GET: api/Nycflights/JFK_Mean_Dep_Arr_delay
        [HttpGet("[action]")]
        public Dictionary<string, string> JFK_Mean_Dep_Arr_delay()
        {
            var context = new FlightDBContext();

            double? averageDepDelayJFK = context.Flights.Where(f => !string.IsNullOrEmpty(f.Origin) && f.Origin.Equals("JFK")).Select(f => f.Dep_delay).ToList().Average();
            double? averageArrDelayJFK = context.Flights.Where(f => !string.IsNullOrEmpty(f.Origin) && f.Origin.Equals("JFK")).Select(f => f.Arr_delay).ToList().Average();

            TimeSpan tavgDepDelayJFK = new TimeSpan();
            TimeSpan tavgArrDelayJFK = new TimeSpan();

            if (averageDepDelayJFK != null)
                tavgDepDelayJFK = TimeSpan.FromMinutes((double)averageDepDelayJFK);

            if (averageArrDelayJFK != null)
                tavgArrDelayJFK = TimeSpan.FromMinutes((double)averageArrDelayJFK);

            return new Dictionary<string, string>() { { tavgDepDelayJFK.TotalSeconds >= 0 ? tavgDepDelayJFK.ToString("hh\\:mm\\:ss") : "-" + tavgDepDelayJFK.ToString("hh\\:mm\\:ss"),
                tavgArrDelayJFK.TotalSeconds >= 0 ? tavgArrDelayJFK.ToString("hh\\:mm\\:ss") : "-" + tavgArrDelayJFK.ToString("hh\\:mm\\:ss") }  };
        }

        //10.2. GET: api/Nycflights/EWR_Mean_Dep_Arr_delay
        [HttpGet("[action]")]
        public Dictionary<string, string> EWR_Mean_Dep_Arr_delay()
        {
            var context = new FlightDBContext();

            double? averageDepDelayEWR = context.Flights.Where(f => !string.IsNullOrEmpty(f.Origin) && f.Origin.Equals("EWR")).Select(f => f.Dep_delay).ToList().Average();
            double? averageArrDelayEWR = context.Flights.Where(f => !string.IsNullOrEmpty(f.Origin) && f.Origin.Equals("EWR")).Select(f => f.Arr_delay).ToList().Average();

            TimeSpan tavgDepDelayEWR = new TimeSpan();
            TimeSpan tavgArrDelayEWR = new TimeSpan();

            if (averageDepDelayEWR != null)
                tavgDepDelayEWR = TimeSpan.FromMinutes((double)averageDepDelayEWR);

            if (averageArrDelayEWR != null)
                tavgArrDelayEWR = TimeSpan.FromMinutes((double)averageArrDelayEWR);

            return new Dictionary<string, string>() { { tavgDepDelayEWR.TotalSeconds >= 0 ? tavgDepDelayEWR.ToString("hh\\:mm\\:ss") : "-" + tavgDepDelayEWR.ToString("hh\\:mm\\:ss"),
                tavgArrDelayEWR.TotalSeconds >= 0 ? tavgArrDelayEWR.ToString("hh\\:mm\\:ss") : "-" + tavgArrDelayEWR.ToString("hh\\:mm\\:ss") }  };
        }

        //10.3. GET: api/Nycflights/LGA_Mean_Dep_Arr_delay
        [HttpGet("[action]")]
        public Dictionary<string, string> LGA_Mean_Dep_Arr_delay()
        {
            var context = new FlightDBContext();

            double? averageDepDelayLGA = context.Flights.Where(f => !string.IsNullOrEmpty(f.Origin) && f.Origin.Equals("LGA")).Select(f => f.Dep_delay).ToList().Average();
            double? averageArrDelayLGA = context.Flights.Where(f => !string.IsNullOrEmpty(f.Origin) && f.Origin.Equals("LGA")).Select(f => f.Arr_delay).ToList().Average();

            TimeSpan tavgDepDelayLGA = new TimeSpan();
            TimeSpan tavgArrDelayLGA = new TimeSpan();

            if (averageDepDelayLGA != null)
                tavgDepDelayLGA = TimeSpan.FromMinutes((double)averageDepDelayLGA);

            if (averageArrDelayLGA != null)
                tavgArrDelayLGA = TimeSpan.FromMinutes((double)averageArrDelayLGA);

            return new Dictionary<string, string>() { { tavgDepDelayLGA.TotalSeconds >= 0 ? tavgDepDelayLGA.ToString("hh\\:mm\\:ss") : "-" + tavgDepDelayLGA.ToString("hh\\:mm\\:ss"),
                tavgArrDelayLGA.TotalSeconds >= 0 ? tavgArrDelayLGA.ToString("hh\\:mm\\:ss") : "-" + tavgArrDelayLGA.ToString("hh\\:mm\\:ss") }  };
        }

        //11. GET: api/Nycflights/MoreThan200ManufaPlanes
        [HttpGet("[action]")]
        public Dictionary<string, int> MoreThan200Manufa_Planes()
        {
            var context = new FlightDBContext();

            return context.Planes.Select(p => p.Manufacturer).ToList()
                .GroupBy(m => m).Where(m => m.Count() >= 200).ToDictionary(g => g.Key, g => g.Count());
        }

        //12. GET: api/Nycflights/Flights_MoreThan200_ManufaPlanes
        [HttpGet("[action]")]
        public Dictionary<string, int> Flights_MoreThan200_ManufaPlanes()
        {
            var context = new FlightDBContext();

            Dictionary<string, int> flights_MoreThan200_ManufaPlanes = new Dictionary<string, int>();
            Dictionary<string, int> moreThan200Manufa_Planes = MoreThan200Manufa_Planes();

            Dictionary<string, int> countByTailNum = context.Flights.Where(f => !string.IsNullOrEmpty(f.Tailnum)).Select(selector: f => f.Tailnum).ToList().GroupBy(m => m).ToDictionary(g => g.Key, g => g.Count());

            foreach (KeyValuePair<string, int> pair in moreThan200Manufa_Planes)
            {
                List<string> tailNums = context.Planes.Where(p => p.Manufacturer.Equals(pair.Key)).Select(p => p.Tailnum).ToList();
                int flights = 0;
                int flightsForTailNum = 0;

                foreach (string tailNum in tailNums)
                {
                    countByTailNum.TryGetValue(tailNum, out flightsForTailNum);
                    flights += flightsForTailNum;
                }

                flights_MoreThan200_ManufaPlanes.Add(pair.Key, flights);
            }

            return flights_MoreThan200_ManufaPlanes;
        }

        //13. GET: api/Nycflights/Airbus_Planes
        [HttpGet("[action]")]
        public Dictionary<string, int> Airbus_Planes()
        {
            var context = new FlightDBContext();

            return context.Planes.Where(p => !string.IsNullOrEmpty(p.Manufacturer) && p.Manufacturer.Contains("AIRBUS"))
                .Select(p => p.Manufacturer).GroupBy(g => g).ToDictionary(g => g.Key, g => context.Planes.Where
                (p => p.Manufacturer.Equals(g.Key)).Select(p => p.Tailnum).Count());
        }

    }
}