using AutoMapper;
using FlightPlanner.Core.Models;
using FlightPlanner.Core.Services;
using FlightPlanner.Data;
using FlightPlanner.Handlers;
using FlightPlanner.Services;
using FlightPlanner.Services.Mappers;
using FlightPlanner.Services.Validators;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;

namespace FlightPlanner
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "FlightPlanner", Version = "v1" });
            });

            services.AddAuthentication("BasicAuthentication")
                .AddScheme<AuthenticationSchemeOptions, BasicAuthenticationHandler>("BasicAuthentication", null);

            services.AddDbContext<FlightPlannerDbContext>(options =>
            {
                options.UseSqlServer(Configuration.GetConnectionString("flight-planner"));
            });

            services.AddTransient<IFlightPlannerDbContext, FlightPlannerDbContext>();
            services.AddTransient<IDbService, DbService>();
            services.AddTransient<IDbExtendedService, DbExtendedService>();
            services.AddTransient<IEntityService<Flight>, EntityService<Flight>>();
            services.AddTransient<IEntityService<Airport>, EntityService<Airport>>();
            services.AddTransient<IFlightService, FlightService>();
            services.AddTransient<IAirportService, AirportService>();
            services.AddTransient<IValidator, AddFlightRequestValidator>();
            services.AddTransient<IValidator, ArrivalTimeValidator>();
            services.AddTransient<IValidator, CarrierValidator>();
            services.AddTransient<IValidator, DepartureTimeValidator>();
            services.AddTransient<IValidator, FromAirportCityValidator>();
            services.AddTransient<IValidator, FromAirportCountryValidator>();
            services.AddTransient<IValidator, FromAirportNameValidator>();
            services.AddTransient<IValidator, FromAirportValidator>();
            services.AddTransient<IValidator, ToAirportCityValidator>();
            services.AddTransient<IValidator, ToAirportCountryValidator>();
            services.AddTransient<IValidator, ToAirportNameValidator>();
            services.AddTransient<IValidator, ToAirportValidator>();
            services.AddTransient<IValidator, AirportNameEqualityValidator>();
            services.AddTransient<IValidator, TimeFrameValidator>();
            var mapper = AutomapperConfig.CreateMapper();
            services.AddSingleton<IMapper>(mapper);

            services.AddCors(o => o.AddPolicy("MyPolicy", builder =>
            {
                builder.WithOrigins("http://localhost:4200")
                .AllowAnyHeader()
                   .AllowCredentials()
                    .AllowAnyMethod();

            }));
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "FlightPlanner v1"));
            }

            app.UseRouting();
            app.UseCors(builder =>
            {
                builder.WithOrigins("http://localhost:4200")
                       .AllowAnyHeader()
                       .AllowCredentials()
                       .AllowAnyMethod();
            });
            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}