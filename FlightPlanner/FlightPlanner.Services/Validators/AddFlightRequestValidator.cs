using FlightPlanner.Core.Dto;
using FlightPlanner.Core.Services;

namespace FlightPlanner.Services.Validators
{
    public class AddFlightRequestValidator : IValidator
    {
        public bool Validate(AddFlightDto dto)
        {
            return dto != null;
        }
    }
}
