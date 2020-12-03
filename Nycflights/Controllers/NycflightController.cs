using Microsoft.AspNetCore.Mvc;
using Nycflights.DataAccessing;
using Nycflights.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Nycflights.Controllers
{
    [Route("api/[controller]")] //initiating api for the entity objects to be monopolied bycontroller
    [ApiController]
    public class NycflightsController : Controller
    {
        //injection to avoid repeatition of calling context method from DataAccess class

        private DBContext _context;

        public NycflightsController(DBContext context)
        {
            _context = context;

        }

        private Dictionary<int, string> monthsByNumber = new Dictionary<int, string>()
            { {1, "Jan" }, {2, "Feb"}, {3, "Mar"},  {4, "Apr"},  {5, "May"},  {6, "Jun"},
                {7, "Jul"},  {8, "Aug"},  {9, "Sep"},  {10, "Oct"},  {11, "Nov"},  {12, "Dec"}};

         #region Flights per month
        //1. GET: api/Nycflights/FlightsPerMonth
        [HttpGet("[action]")]
        public Dictionary<string, int> FlightsPerMonth()
        {
            return _context.Flights.Select(f => f.Month).ToList().GroupBy(m => m).OrderBy(g => g.Key).ToDictionary(g => monthsByNumber[g.Key], g => g.Count());
        }


        //2.1. GET: api/Nycflights/FlightsPerMonthForJFK
        [HttpGet("[action]")]
        public Dictionary<string, int> FlightsPerMonthForJFK()
        {
            return _context.Flights.Where(f => !string.IsNullOrEmpty(f.Origin) && f.Origin.Equals("JFK")).Select(f => f.Month).ToList()
                .GroupBy(m => m).OrderBy(g => g.Key).ToDictionary(g => monthsByNumber[g.Key], g => g.Count());
        }

        //2.2. GET: api/Nycflights/FlightsPerMonthForEWR
        [HttpGet("[action]")]
        public Dictionary<string, int> FlightsPerMonthForEWR()
        {
            return _context.Flights.Where(f => !string.IsNullOrEmpty(f.Origin) && f.Origin.Equals("EWR")).Select(f => f.Month).ToList()
                .GroupBy(m => m).OrderBy(g => g.Key).ToDictionary(g => monthsByNumber[g.Key], g => g.Count());
        }

        
        //2.3. GET: api/Nycflights/FlightsPerMonthForLGA
        [HttpGet("[action]")]
        public Dictionary<string, int> FlightsPerMonthForLGA()
        {
            return _context.Flights.Where(f => !string.IsNullOrEmpty(f.Origin) && f.Origin.Equals("LGA")).Select(f => f.Month).ToList()
                .GroupBy(m => m).OrderBy(g => g.Key).ToDictionary(g => monthsByNumber[g.Key], g => g.Count());
        }
        #endregion


        #region Top ten destinations
        //3.1. GET: api/Nycflights/FlightsToTopTenDestinations
        [HttpGet("[action]")]
        public Dictionary<string, int> FlightsToTopTenDestinations()
        {
            return _context.Flights.Select(f => f.Dest).ToList().GroupBy(d => d).ToDictionary(g => g.Key, g => g.Count())
                .OrderByDescending(val => val.Value).Take(10).ToDictionary(g => g.Key, g => g.Value);
        }

        //3.2. GET: api/Nycflights/FlightsToTopTenDestinationsFromJFK
        [HttpGet("[action]")]
        public Dictionary<string, int> FlightsToTopTenDestinationsFromJFK()
        {

            List<string> topTenDestinations = _context.Flights.Select(f => f.Dest).ToList().GroupBy(d => d).ToDictionary(g => g.Key, g => g.Count())
                .OrderByDescending(val => val.Value).Take(10).ToDictionary(g => g.Key, g => g.Value).Keys.ToList();

            Dictionary<string, int> flightsToTopTenDestinationsFromJFK = new Dictionary<string, int>();
            foreach (string dest in topTenDestinations)
            {
                flightsToTopTenDestinationsFromJFK.Add(dest, _context.Flights.Where(f => !string.IsNullOrEmpty(f.Origin) && f.Origin.Equals("JFK") &&
                    !string.IsNullOrEmpty(f.Dest) && f.Dest.Equals(dest)).Count());
            }

            return flightsToTopTenDestinationsFromJFK;
        }

        //3.3. GET: api/Nycflights/FlightsToTopTenDestinationsFromEWR
        [HttpGet("[action]")]
        public Dictionary<string, int> FlightsToTopTenDestinationsFromEWR()
        {
            List<string> topTenDestinations = _context.Flights.Select(f => f.Dest).ToList().GroupBy(d => d).ToDictionary(g => g.Key, g => g.Count())
                .OrderByDescending(val => val.Value).Take(10).ToDictionary(g => g.Key, g => g.Value).Keys.ToList();

            Dictionary<string, int> flightsToTopTenDestinationsFromEWR = new Dictionary<string, int>();
            foreach (string dest in topTenDestinations)
            {
                flightsToTopTenDestinationsFromEWR.Add(dest, _context.Flights.Where(f => !string.IsNullOrEmpty(f.Origin) && f.Origin.Equals("EWR") &&
                    !string.IsNullOrEmpty(f.Dest) && f.Dest.Equals(dest)).Count());
            }

            return flightsToTopTenDestinationsFromEWR;
        }

        //3.4. GET: api/Nycflights/FlightsToTopTenDestinationsFromLGA
        [HttpGet("[action]")]
        public Dictionary<string, int> FlightsToTopTenDestinationsFromLGA()
        {
            List<string> topTenDestinations = _context.Flights.Select(f => f.Dest).ToList().GroupBy(d => d).ToDictionary(g => g.Key, g => g.Count())
                .OrderByDescending(val => val.Value).Take(10).ToDictionary(g => g.Key, g => g.Value).Keys.ToList();

            Dictionary<string, int> flightsToTopTenDestinationsFromLGA = new Dictionary<string, int>();
            foreach (string dest in topTenDestinations)
            {
                flightsToTopTenDestinationsFromLGA.Add(dest, _context.Flights.Where(f => !string.IsNullOrEmpty(f.Origin) && f.Origin.Equals("LGA") &&
                    !string.IsNullOrEmpty(f.Dest) && f.Dest.Equals(dest)).Count());
            }

            return flightsToTopTenDestinationsFromLGA;
        }
        #endregion



    }
}
