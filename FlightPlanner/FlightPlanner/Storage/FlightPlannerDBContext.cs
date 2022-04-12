﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FlightPlanner.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace FlightPlanner.Storage
{
    public class FlightPlannerDBContext : DbContext
    {
       public DbSet<Flight> Flights { get; set; }

       public DbSet<Airport> Airports { get; set;  }

       protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
       {
           IConfigurationRoot configuration = new ConfigurationBuilder()
               .SetBasePath(AppDomain.CurrentDomain.BaseDirectory)
               .AddJsonFile("appsettings.json")
               .Build();
           optionsBuilder.UseSqlServer(configuration.GetConnectionString("DefaultConnection"));
       }
    }
}