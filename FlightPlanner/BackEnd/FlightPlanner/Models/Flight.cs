﻿namespace FlightPlanner.Models
{
    public class Flight
    {
        public int Id { get; set; }
        public Airport From { get; set; }
        public Airport To { get; set; }
        public string Carrier { get; set; }
        public string DepartureTime { get; set; }
        public string ArrivalTime { get; set; }

        public bool Equals(Flight flightToCheck)
        {
            return flightToCheck.From.Equals(From)
                   && flightToCheck.To.Equals(To)
                   && flightToCheck.Carrier == Carrier
                   && flightToCheck.DepartureTime == DepartureTime
                   && flightToCheck.ArrivalTime == ArrivalTime;
        }
    }
}