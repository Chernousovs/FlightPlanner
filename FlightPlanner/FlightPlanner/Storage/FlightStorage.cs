using FlightPlanner.Models;
using FlightPlanner.UserDefinedExeptions;
using System;
using System.Collections.Generic;
using System.Linq;

namespace FlightPlanner.Storage
{
    public static class FlightStorage
    {
        private static readonly object flightLock = new object();
        private static List<Flight> flights = new List<Flight>();
        private static int id; //last successful added flight ID

        public static Flight AddFlight(AddFlightRequest request)
        {

            if (request.From == null
                || request.From.HasIncorrectValues()
                || request.To == null
                || request.To.HasIncorrectValues()
                || request.ArrivalTime == null
                || request.DepartureTime == null
                || string.IsNullOrEmpty(request.Carrier))
            {
                throw new FlightsIncorrectDataException("One or more fields are null");
            }

            if (request.From.Equals(request.To))
            {
                throw new FlightsIncorrectDataException("Airports are same");
            }

            if (DateTime.Compare(DateTime.Parse(request.DepartureTime), DateTime.Parse(request.ArrivalTime)) == 0
                || DateTime.Compare(DateTime.Parse(request.DepartureTime), DateTime.Parse(request.ArrivalTime)) > 0)
            {
                throw new FlightsIncorrectDataException("Incorrect Arrival or Departure time");
            }

            var flight = new Flight
            {
                From = request.From,
                To = request.To,
                ArrivalTime = request.ArrivalTime,
                DepartureTime = request.DepartureTime,
                Carrier = request.Carrier,
                Id = ++id
            };

            lock (flightLock)
            {
                if (flights.Count > 0)
                {
                    if (FlightAlreadyExists(flight))
                    {
                        return null;
                    }
                }

                flights.Add(flight);
            }

            return flight;
        }

        public static Flight GetFlight(int id)
        {
            return flights.FirstOrDefault(o => o.Id == id);
        }

        public static List<Airport> SearchAirports(string search)
        {
            var airportList = new List<Airport>();
            airportList.AddRange(flights.Select(o => o.From));
            airportList.AddRange(flights.Select(o => o.To));

            return airportList.Where(o => o.AirportName.ToUpper().Contains(search.Trim().ToUpper())
                                            || o.City.ToUpper().Contains(search.Trim().ToUpper())
                                            || o.Country.ToUpper().Contains(search.Trim().ToUpper())).ToList();
        }

        //"should return no results when nothing found"
        public static List<Flight> FlightSearch(SearchFlightsRequest req)
        {
            if (req.From == null || req.To == null || req.DepartureDate == null)
            {
                throw new FlightsIncorrectDataException("One or more fields are null");
            }

            if (req.From == req.To)
            {
                throw new FlightsIncorrectDataException("Airports are same");
            }

            lock (flightLock)
            {
                return flights.Where(o => o.From.AirportName == req.From
                                          && o.To.AirportName == req.To
                                          && DateTime.Parse(o.DepartureTime).Date == DateTime.Parse(req.DepartureDate)).ToList();
            }

        }

        public static void RemoveFlight(int id)
        {
            lock (flightLock)
            {
                var flightToDelete = flights.FirstOrDefault(x => x.Id == id);
                if (flightToDelete != null)
                {
                    flights.Remove(flightToDelete);
                }
            }
        }

        public static void ClearFlights()
        {
            flights.Clear();
            id = 0;
        }

        private static bool FlightAlreadyExists(Flight flight)
        {
            return flights.Any(o => o.Equals(flight));
        }
    }
}