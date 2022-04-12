using AutoMapper;
using FlightPlanner.Core.Dto;
using FlightPlanner.Core.Models;

namespace FlightPlanner.Services.Mappers
{
    public class AutomapperConfig
    {
        public static IMapper CreateMapper()
        {
            var configuration = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<Flight, FlightDto>();
                cfg.CreateMap<FlightDto, Flight>();

                cfg.CreateMap<Airport, AirportDto>()
                    .ForMember(
                        dest => dest.Airport,
                    opt =>
                        opt.MapFrom(a => a.AirportName));
                cfg.CreateMap<AirportDto, Airport>()
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