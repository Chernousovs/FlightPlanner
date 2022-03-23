using System.Runtime.InteropServices.ComTypes;
using AutoMapper;
using FlightPlanner.Core.Dto;
using FlightPlanner.Models;

namespace FlightPlanner.Services.Mappers
{
    public class AutomapperConfig
    {
        public static IMapper CreateMapper()
        {
            var configuration = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<Flight, AddFlightDto>();
                cfg.CreateMap<AddFlightDto, Flight>();

                cfg.CreateMap<Airport, AddAirportDto>()
                    .ForMember(
                        dest => dest.Airport,
                    opt => 
                        opt.MapFrom(a => a.AirportName)); 
                cfg.CreateMap<AddAirportDto, Airport>()
                    .ForMember(
                    dest => dest.AirportName,
                    opt => 
                        opt.MapFrom(a => a.Airport))
                    .ForMember(
                        dest => dest.Id,
                        opt => 
                            opt.Ignore());
            });
            //only during development, validate your mapping s; remove it before release
            configuration.AssertConfigurationIsValid();

            var mapper = configuration.CreateMapper();
            return mapper;
        }
    }
}
