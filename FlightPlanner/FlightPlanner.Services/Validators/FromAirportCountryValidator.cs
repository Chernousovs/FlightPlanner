using FlightPlanner.Core.Dto;
using FlightPlanner.Core.Services;

namespace FlightPlanner.Services.Validators
{
    public class FromAirportCountryValidator : IValidator
    {
        public bool Validate(AddFlightDto dto)
        {
            return !string.IsNullOrEmpty(dto?.From?.Country);
        }
    }
}
