using FlightPlanner.Core.Services;
using FlightPlanner.Data;
using FlightPlanner.Storage;
using Microsoft.AspNetCore.Mvc;

namespace FlightPlanner.Controllers
{
    [Route("testing-api")]
    [ApiController]
    public class TestingController : ControllerBase
    {
        private readonly IDbExtendedService _service;

        public TestingController(IDbExtendedService service)
        {
            _service = service;
        }

        [HttpPost]
        [Route("clear")]
        public IActionResult Clear()
        {
            _service.DeleteAll();
            return Ok();
        }
    }
}