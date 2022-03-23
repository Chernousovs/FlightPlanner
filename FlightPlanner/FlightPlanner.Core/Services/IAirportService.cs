using System.Collections.Generic;
using FlightPlanner.Core.Dto;
using FlightPlanner.Models;

namespace FlightPlanner.Core.Services
{
    public interface IAirportService : IEntityService<Airport>
    {
        List<Airport> SearchAirports(string search);
    }
}
