using FlightPlanner.Core.Dto;
using FlightPlanner.Core.Services;
using System;

namespace FlightPlanner.Services.Validators
{
    public class TimeFrameValidator : IValidator
    {
        public bool Validate(FlightDto dto)
        {
            var arrivalDate = DateTime.Parse(dto.ArrivalTime);
            var departureDate = DateTime.Parse(dto.DepartureTime);

            return arrivalDate > departureDate;
        }
    }
}