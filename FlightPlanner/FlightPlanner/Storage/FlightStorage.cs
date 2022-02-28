using FlightPlanner.Models;
using FlightPlanner.UserDefinedExeptions;
using System;
using System.Collections.Generic;
using System.Linq;

namespace FlightPlanner.Storage
{
    public static class FlightStorage
    {
        private static readonly object _flightLock = new object();
        private static List<Flight> _flights = new List<Flight>();
        private static int _id; //last successful added flight ID

        public static bool IsValidFlightToAdd(AddFlightRequest request)
        {
            lock (_flightLock)
            {
                if (request.From == null
                    || request.From.HasIncorrectValues()
                    || request.To == null
                    || request.To.HasIncorrectValues()
                    || request.ArrivalTime == null
                    || request.DepartureTime == null
                    || string.IsNullOrEmpty(request.Carrier))
                {
                    return false;
                }

                if (request.From.Equals(request.To))
                {
                    return false;
                }

                if (DateTime.Compare(DateTime.Parse(request.DepartureTime), DateTime.Parse(request.ArrivalTime)) == 0
                    || DateTime.Compare(DateTime.Parse(request.DepartureTime), DateTime.Parse(request.ArrivalTime)) > 0)
                {
                    return false;
                }

                return true;
            }
        }

        public static Flight ConvertToFlight(AddFlightRequest request)
        {
            var flight = new Flight
            {
                From = request.From,
                To = request.To,
                ArrivalTime = request.ArrivalTime,
                DepartureTime = request.DepartureTime,
                Carrier = request.Carrier
            };

            return flight;
        }

        public static Flight GetFlight(int id)
        {
            return _flights.FirstOrDefault(o => o.Id == id);
        }

        public static List<Airport> SearchAirports(string search)
        {
            var airportList = new List<Airport>();
            airportList.AddRange(_flights.Select(o => o.From));
            airportList.AddRange(_flights.Select(o => o.To));

            return airportList.Where(o => o.AirportName.ToUpper().Contains(search.Trim().ToUpper())
                                            || o.City.ToUpper().Contains(search.Trim().ToUpper())
                                            || o.Country.ToUpper().Contains(search.Trim().ToUpper())).ToList();
        }

        //"should return no results when nothing found"
        public static bool IsValidRequest(SearchFlightsRequest req)
        {
            if (req.From == null || req.To == null || req.DepartureDate == null)
            {
                return false;
            }

            if (req.From == req.To)
            {
                return false;
            }

            return true;
        }

        public static void RemoveFlight(int id)
        {
            lock (_flightLock)
            {
                var flightToDelete = _flights.FirstOrDefault(x => x.Id == id);
                if (flightToDelete != null)
                {
                    _flights.Remove(flightToDelete);
                }
            }
        }

        public static void ClearFlights()
        {
            _flights.Clear();
            _id = 0;
        }

        private static bool FlightAlreadyExists(Flight flight)
        {
            return _flights.Any(o => o.Equals(flight));
        }
    }
}