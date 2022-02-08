using FlightPlanner.Models;
using FlightPlanner.Storage;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;

namespace FlightPlanner.Controllers
{
    [Route("admin-api")]
    [ApiController]
    [Authorize]
    public class AdminApiController : ControllerBase
    {
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

        [HttpPut]
        [Route("Flights")]
        public IActionResult PutFlights(AddFlightRequest request)
        {
            try
            {
                var flight = FlightStorage.AddFlight(request);

                if (flight != null)
                {
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