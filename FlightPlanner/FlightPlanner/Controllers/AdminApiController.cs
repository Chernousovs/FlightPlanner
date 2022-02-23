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
                //FlightStorage.GetFlight(id);
            if (flight != null)
            {
                return Ok(flight);
            }

            return NotFound();
        }

        [HttpPut]
        [Route("Flights")]
        public IActionResult PutFlights(AddFlightRequest request)
        {
            try
            {
                var flight = FlightStorage.AddFlight(request);

                if (flight != null)
                {
                    _context.Flights.Add(FlightStorage.ConvertToFlight(request));
                    _context.SaveChanges();
                    return Created("", flight);
                }

                return Conflict();
            }
            catch (Exception)
            {
                return BadRequest();
            }
        }

        [HttpDelete]
        [Route("Flights/{id}")]
        public IActionResult DeleteFlight(int id)
        {
            FlightStorage.RemoveFlight(id);
            return Ok();
        }
    }
}