using FlightPlanner.Core.Dto;
using FlightPlanner.Core.Services;
using System;

namespace FlightPlanner.Services.Validators
{
    public class AirportNameEqualityValidator : IValidator
    {
        public bool Validate(FlightDto dto)
        {
            return !string.Equals(dto.From.Airport.Trim(), dto.To.Airport.Trim(),
                StringComparison.CurrentCultureIgnoreCase);
        }
    }
}