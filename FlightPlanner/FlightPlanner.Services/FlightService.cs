using System.Collections.Generic;
using System.Linq;
using FlightPlanner.Core.Dto;
using FlightPlanner.Core.Services;
using FlightPlanner.Data;
using FlightPlanner.Models;
using Microsoft.EntityFrameworkCore;

namespace FlightPlanner.Services
{
    public class FlightService : EntityService<Flight>, IFlightService
    {
        public FlightService(IFlightPlannerDbContext context) : base(context)
        {
        }

        public Flight GetFlightWithAirports(int id)
        {
            return Query()
                .Include(f => f.From)
                .Include(f => f.To)
                .SingleOrDefault(f => f.Id == id);
        }

        public void DeleteFlightById(int id)
        {
            var flight = GetFlightWithAirports(id);
            if (flight != null)
            {
                Delete(flight);
            }
        }

        public bool FlightAlreadyExistsInDb(AddFlightDto dto)
        {
            return Query().Any(f => f.ArrivalTime == dto.ArrivalTime 
                                    && f.DepartureTime == dto.DepartureTime
                                    && f.Carrier == dto.Carrier
                                    && f.From.AirportName.Trim().ToLower() == dto.From.Airport.Trim().ToLower()
                                    && f.To.AirportName.Trim().ToLower() == dto.To.Airport.Trim().ToLower());
        }
    }
}
