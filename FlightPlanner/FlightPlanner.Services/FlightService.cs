using FlightPlanner.Core.Dto;
using FlightPlanner.Core.Models;
using FlightPlanner.Core.Services;
using FlightPlanner.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;

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

        public bool FlightAlreadyExistsInDb(FlightDto dto)
        {
            return Query().Any(f => f.ArrivalTime == dto.ArrivalTime
                                    && f.DepartureTime == dto.DepartureTime
                                    && f.Carrier == dto.Carrier
                                    && string.Equals(f.From.AirportName.Trim().ToLower(), dto.From.Airport.Trim().ToLower())
                                    && string.Equals(f.To.AirportName.Trim().ToLower(), dto.To.Airport.Trim().ToLower()));
        }

        public bool IsValidFlightSearchRequest(FlightSearchRequest req)
        {
            return req.From != null && req.To != null && req.DepartureDate != null && req.From != req.To;
        }

        public List<Flight> SearchFlightsByCriteria(FlightSearchRequest req)
        {
            return Query()
                .Include(f => f.From)
                .Include(f => f.To)
                .ToList()
                .Where(o => o.From.AirportName == req.From
                            && o.To.AirportName == req.To
                            && DateTime.Parse((string)o.DepartureTime).Date == DateTime.Parse(req.DepartureDate)).ToList();
        }
    }
}