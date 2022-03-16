using System;
using System.Text.Json.Serialization;
using FlightPlanner.Core.Models;

namespace FlightPlanner.Models
{
    public class Airport : Entity
    {
        public string Country { get; set; }
        public string City { get; set; }
        public string AirportName { get; set; }

        public override bool Equals(object o)
        {
            return o is Airport airport
                   && string.Equals(airport.AirportName.Trim(), AirportName.Trim(), StringComparison.CurrentCultureIgnoreCase);
        }

        public bool HasIncorrectValues()
        {
            return string.IsNullOrEmpty(Country)
                   || string.IsNullOrEmpty(City)
                   || string.IsNullOrEmpty(AirportName);
        }
    }
}