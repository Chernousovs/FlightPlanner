﻿using FlightPlanner.Core.Dto;
using FlightPlanner.Core.Services;

namespace FlightPlanner.Services.Validators
{
    public class ToAirportCityValidator : IValidator
    {
        public bool Validate(FlightDto dto)
        {
            return !string.IsNullOrEmpty(dto?.To?.City);
        }
    }
}