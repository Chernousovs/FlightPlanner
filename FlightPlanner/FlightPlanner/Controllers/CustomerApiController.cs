using FlightPlanner.Models;
using FlightPlanner.Storage;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using FlightPlanner.Core.Dto;
using FlightPlanner.Core.Services;
using FlightPlanner.Data;
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
        private readonly IFlightPlannerDbContext _context;
        private readonly IFlightService _flightService;
        private readonly IAirportService _airportService;
        private readonly IEnumerable<IValidator> _validators;
        private readonly IMapper _mapper;

        public CustomerApiController(
            IFlightPlannerDbContext context, 
            IFlightService flightService,
            IAirportService airportService,
            IEnumerable<IValidator> validators,
            IMapper mapper)
        {
            _context = context;
            _flightService = flightService;
            _airportService = airportService;
            _validators = validators;
            _mapper = mapper;
        }

        [HttpGet]
        [Route("airports")]
        public IActionResult SearchAirports(string search)
        {
            lock (_flightLock)
            {
                //var airport = new List<Airport>();;
                //airport.AddRange((IEnumerable<Airport>)_context.Flights.Select(f => f.From));
                //airport.AddRange((IEnumerable<Airport>)_context.Flights.Select(f => f.To));

                var searchResultAirports = _airportService.SearchAirports(search);

                if (searchResultAirports.Any())
                {
                    List<AddAirportDto> t = searchResultAirports.Select(o => _mapper.Map<AddAirportDto>(o)).ToList();
                    return Ok(t);
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
                                && DateTime.Parse((string) o.DepartureTime).Date == DateTime.Parse(req.DepartureDate)).ToList();

                var pageResult = new PageResult<AddFlightDto>
                {
                    Page = 0,
                    TotalItems = flightSearchResult.Count(),
                    //Items = flightSearchResult
                    Items = flightSearchResult.Select(o =>_mapper.Map<AddFlightDto>(o)).ToList()
                };

                return Ok(pageResult);
            }
        }

        [HttpGet]
        [Route("flights/{id:int}")]
        public IActionResult GetFlights(int id)
        {
            var flight = _flightService.GetFlightWithAirports(id);

            return flight == null ? NotFound() : (IActionResult) Ok(_mapper.Map<AddFlightDto>(flight));
        }
    }
}