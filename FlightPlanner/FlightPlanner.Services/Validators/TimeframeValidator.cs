using System;
using FlightPlanner.Core.Dto;
using FlightPlanner.Core.Services;

namespace FlightPlanner.Services.Validators
{
    public class TimeframeValidator :IValidator
    {
        public bool Validate(AddFlightDto dto)
        {
            var airrivalDate = DateTime.Parse(dto.ArrivalTime);
            var departureDate = DateTime.Parse(dto.DepartureTime);

            return airrivalDate > departureDate;
        }
    }
}
