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



    }
}
#endregion