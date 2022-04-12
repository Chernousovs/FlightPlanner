using AutoMapper;
using FlightPlanner.Core.Dto;
using FlightPlanner.Core.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;

namespace FlightPlanner.Controllers
{
    [Route("admin-api")]
    [EnableCors]
    [ApiController]
    [Authorize]
    public class AdminApiController : ControllerBase
    {
        private readonly object _flightLock = new object();
        private readonly IFlightService _flightService;
        private readonly IEnumerable<IValidator> _validators;
        private readonly IMapper _mapper;

        public AdminApiController(
            IFlightService flightService,
            IEnumerable<IValidator> validators,
            IMapper mapper)
        {
            _flightService = flightService;
            _validators = validators;
            _mapper = mapper;
        }

        [HttpGet]
        [Route("flights/{id:int}")]
        public IActionResult GetFlights(int id)
        {
            var flight = _flightService.GetFlightWithAirports(id);

            return flight == null ? NotFound() : (IActionResult)Ok(flight);
        }

        [HttpPut]
        [Route("Flights")]
        public IActionResult PutFlights(FlightDto dto)
        {
            if (!_validators.All(v => v.Validate(dto)))
            {
                return BadRequest();
            }

            lock (_flightLock)
            {
                if (_flightService.FlightAlreadyExistsInDb(dto))
                {
                    return Conflict();
                }

                var flight = _mapper.Map<Flight>(dto);
                _flightService.Create(flight);

                return Created("", _mapper.Map<FlightDto>(flight));
            }
        }

        [HttpDelete]
        [Route("Flights/{id}")]
        public IActionResult DeleteFlight(int id)
        {
            lock (_flightLock)
            {
                _flightService.DeleteFlightById(id);

                return Ok();
            }
        }
    }
}