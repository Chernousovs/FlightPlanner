using FlightPlanner.Models;
using FlightPlanner.Storage;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Cors;
using Microsoft.EntityFrameworkCore;

namespace FlightPlanner.Controllers
{
    [Route("api")]
    [EnableCors]
    [ApiController]
    public class CustomerApiController : ControllerBase
    {
        private static readonly object _flightLock = new object();
        private readonly FlightPlannerDBContext _context;

        public CustomerApiController(FlightPlannerDBContext context)
        {
            _context = context;
        }

        [HttpGet]
        [Route("airports")]
        public IActionResult SearchAirports(string search)
        {
            lock (_flightLock)
            {
                var airport = new List<Airport>();
                airport.AddRange(_context.Flights.Select(f => f.From));
                airport.AddRange(_context.Flights.Select(f => f.To));

                var searchResultAirports = airport.Where(f => f.AirportName.ToUpper().Contains(search.Trim().ToUpper()) 
                                                              || f.City.ToUpper().Contains(search.Trim().ToUpper()) 
                                                              || f.Country.ToUpper().Contains(search.Trim().ToUpper()));
                if (searchResultAirports.Any())
                {
                    return Ok(searchResultAirports);
                }

                return NotFound();
            }
        }

        [HttpPost]
        [Route("flights/search")]
        public IActionResult SearchFlights(SearchFlightsRequest req)
        {
            if (!FlightStorage.IsValidRequest(req))
            {
                return BadRequest();
            }

            lock (_flightLock)
            {
                var flightSearchResult = _context.Flights
                    .Include(f => f.From)
                    .Include(f => f.To)
                    .ToList()
                    .Where(o => o.From.AirportName == req.From
                                && o.To.AirportName == req.To
                                && DateTime.Parse(o.DepartureTime).Date == DateTime.Parse(req.DepartureDate)).ToList();

                var pageResult = new PageResult<Flight>
                {
                    Page = 0,
                    TotalItems = flightSearchResult.Count,
                    Items = flightSearchResult
                };

                return Ok(pageResult);
            }
        }

        [HttpGet]
        [Route("flights/{id:int}")]
        public IActionResult GetFlights(int id)
        {
            var flight = _context.Flights
                .Include(f=> f.From)
                .Include(f => f.To)
                .SingleOrDefault(f => f.Id == id);
           
            if (flight != null)
            {
                return Ok(flight);
            }

            return NotFound();
        }
    }
}