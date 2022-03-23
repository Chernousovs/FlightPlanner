using FlightPlanner.Core.Dto;
using FlightPlanner.Core.Services;

namespace FlightPlanner.Services.Validators
{
    public class FromAirportCityValidator : IValidator
    {
        public bool Validate(FlightDto dto)
        {
            return !string.IsNullOrEmpty(dto?.From?.City);
        }
    }
}