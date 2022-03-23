
using System;
using FlightPlanner.Core.Dto;
using FlightPlanner.Core.Services;

namespace FlightPlanner.Services.Validators
{
    public class AirportNameEqualityValidator : IValidator
    {
        public bool Validate(AddFlightDto dto)
        {
            return !string.Equals(dto.From.Airport.Trim(), dto.To.Airport.Trim(),
                StringComparison.CurrentCultureIgnoreCase);
        }
    }
}
