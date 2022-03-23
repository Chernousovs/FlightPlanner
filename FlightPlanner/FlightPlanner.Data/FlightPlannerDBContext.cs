 using System;
 using System.Threading.Tasks;
 using FlightPlanner.Models;
 using Microsoft.EntityFrameworkCore;

namespace FlightPlanner.Data
{
    public class FlightPlannerDBContext : DbContext, IFlightPlannerDbContext
    {
        public FlightPlannerDBContext(DbContextOptions options) : base(options) { }

        public DbSet<Flight> Flights { get; set; }

        public DbSet<Airport> Airports { get; set; }

        public async Task<int> SaveChangesAsync()
        {
            return await base.SaveChangesAsync();
        }

        
    }
}
