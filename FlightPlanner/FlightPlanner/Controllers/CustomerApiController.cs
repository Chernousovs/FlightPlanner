using FlightPlanner.Models;
using FlightPlanner.Storage;
using Microsoft.AspNetCore.Mvc;
using System;
using Microsoft.AspNetCore.Cors;

namespace FlightPlanner.Controllers
{
    [Route("api")]
    [EnableCors]
    [ApiController]
    public class CustomerApiController : ControllerBase
    {
        [HttpGet]
        [Route("airports")]
        public IActionResult SearchAirports(string search)
        {
            var airport = FlightStorage.SearchAirports(search);
            if (airport != null)
            {
                return Ok(airport);
            }

            return NotFound();
        }

        [HttpPost]
        [Route("flights/search")]
        public IActionResult SearchFlights(SearchFlightsRequest req)
        {
            try
            {
                var flightsList = FlightStorage.FlightSearch(req);
                var pageResult = new PageResult<Flight>
                {
                    Page = 0,
                    TotalItems = flightsList.Count,
                    Items = flightsList
                };

                return Ok(pageResult);
            }
            catch (Exception)
            {
                return BadRequest();
            }
        }

        [HttpGet]
        [Route("flights/{id:int}")]
        public IActionResult GetFlights(int id)
        {
            var flight = FlightStorage.GetFlight(id);
            if (flight != null)
            {
                return Ok(flight);
            }

            return NotFound();
        }
    }
}