using FlightPlanner.Storage;
using Microsoft.AspNetCore.Mvc;

namespace FlightPlanner.Controllers
{
    [Route("testing-api")]
    [ApiController]
    public class TestingController : ControllerBase
    {
        private readonly FlightPlannerDBContext _context;

        public TestingController(FlightPlannerDBContext context)
        {
            _context = context;
        }

        [HttpPost]
        [Route("clear")]
        public IActionResult Clear()
        {
           
            _context.Flights.RemoveRange(_context.Flights);
            _context.Airports.RemoveRange(_context.Airports);
            _context.SaveChanges();

            return Ok();
        }
    }
}