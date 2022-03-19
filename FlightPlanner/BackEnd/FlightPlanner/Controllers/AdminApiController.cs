using FlightPlanner.Models;
using FlightPlanner.Storage;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;
using Microsoft.AspNetCore.Cors;
using Microsoft.EntityFrameworkCore;

namespace FlightPlanner.Controllers
{
    [Route("admin-api")]
    [EnableCors]
    [ApiController]
    [Authorize]
    public class AdminApiController : ControllerBase
    {
        private static readonly object _flightLock = new object();
        private readonly FlightPlannerDBContext _context;

        public AdminApiController(FlightPlannerDBContext context)
        {
            _context = context;
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

        [HttpPut]
        [Route("flights")]
        public IActionResult PutFlights(AddFlightRequest request)
        {
            lock (_flightLock)
            {

                if (!FlightStorage.IsValidFlightToAdd(request))
                {
                    return BadRequest();
                }

                if (FlightAlreadyExistsInDB(request))
                {
                    return Conflict();
                }

                _context.Flights.Add(FlightStorage.ConvertToFlight(request));
                _context.SaveChanges();
                return Created("",  _context.Flights.OrderBy(o => o.Id).Last());
            }
       
        }

        [HttpDelete]
        [Route("flights/{id}")]
        public IActionResult DeleteFlight(int id)
        {
            lock (_flightLock)
            {
                var flight = _context.Flights
                    .Include(f => f.From)
                    .Include(f => f.To)
                    .SingleOrDefault(f => f.Id == id);
            
                if (flight == null)
                {
                    return Ok();
                }

                _context.Flights.Remove(flight);
                _context.SaveChanges();

                return Ok();
            }
            
        }

        private bool FlightAlreadyExistsInDB(AddFlightRequest request)
        {
            return _context.Flights.Any(f => f.ArrivalTime == request.ArrivalTime &&
                                             f.DepartureTime == request.DepartureTime &&
                                             f.From.AirportName.Trim().ToLower() == request.From.AirportName.Trim().ToLower() &&
                                             f.To.AirportName.Trim().ToLower() == request.To.AirportName.Trim().ToLower());
        }
    }
}