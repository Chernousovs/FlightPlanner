using AutoMapper;
using FlightPlanner.Core.Dto;
using FlightPlanner.Core.Services;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using System.Linq;

namespace FlightPlanner.Controllers
{
    [Route("api")]
    [EnableCors]
    [ApiController]
    public class CustomerApiController : ControllerBase
    {
        private static readonly object FlightLock = new object();
        private readonly IFlightService _flightService;
        private readonly IAirportService _airportService;
        private readonly IMapper _mapper;

        public CustomerApiController(
            IFlightService flightService,
            IAirportService airportService,
            IMapper mapper)
        {
            _flightService = flightService;
            _airportService = airportService;
            _mapper = mapper;
        }

        [HttpGet]
        [Route("airports")]
        public IActionResult SearchAirports(string search)
        {
            lock (FlightLock)
            {
                var searchResultAirports = _airportService.SearchAirports(search);

                return searchResultAirports.Any() ?
                    Ok(searchResultAirports.Select(o => _mapper.Map<AirportDto>(o)).ToList()) : NotFound();
            }
        }

        [HttpPost]
        [Route("flights/search")]
        public IActionResult SearchFlights(FlightSearchRequest req)
        {
            if (!_flightService.IsValidFlightSearchRequest(req))
            {
                return BadRequest();
            }

            lock (FlightLock)
            {
                var flightSearchResult = _flightService.SearchFlightsByCriteria(req);

                var pageResult = new PageResult<FlightDto>
                {
                    Page = 0,
                    TotalItems = flightSearchResult.Count(),
                    Items = flightSearchResult.Select(o => _mapper.Map<FlightDto>(o)).ToList()
                };

                return Ok(pageResult);
            }
        }

        [HttpGet]
        [Route("flights/{id:int}")]
        public IActionResult GetFlights(int id)
        {
            var flight = _flightService.GetFlightWithAirports(id);

            return flight == null ? NotFound() : (IActionResult)Ok(_mapper.Map<FlightDto>(flight));
        }
    }
}