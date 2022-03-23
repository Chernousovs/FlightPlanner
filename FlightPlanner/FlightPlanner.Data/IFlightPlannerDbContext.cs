using System.Threading.Tasks;
using FlightPlanner.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace FlightPlanner.Data
{
    public interface IFlightPlannerDbContext
    {
        DbSet<T> Set<T>() where T: class ;
        EntityEntry<T> Entry<T>(T entuty) where T : class;
        DbSet<Flight> Flights { get; set; }
        DbSet<Airport> Airports { get; set; }

        int SaveChanges();

        Task<int> SaveChangesAsync();
    }
}
