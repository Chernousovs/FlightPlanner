using FlightPlanner.Core.Dto;
using FlightPlanner.Core.Models;
using System.Collections.Generic;

namespace FlightPlanner.Core.Services
{
    public interface IFlightService : IEntityService<Flight>
    {
        Flight GetFlightWithAirports(int id);
        void DeleteFlightById(int id);
        bool FlightAlreadyExistsInDb(FlightDto dto);
        bool IsValidFlightSearchRequest(FlightSearchRequest req);
        List<Flight> SearchFlightsByCriteria(FlightSearchRequest req);
    }
}