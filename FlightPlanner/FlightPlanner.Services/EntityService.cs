﻿using FlightPlanner.Core.Models;
using FlightPlanner.Core.Services;
using System.Collections.Generic;
using System.Linq;
using FlightPlanner.Data;

namespace FlightPlanner.Services
{
    public class EntityService<T> : DbService, IEntityService<T> where T : Entity
    {
        public EntityService(IFlightPlannerDbContext context) : base(context)
        {
        }

        public void Create(T entity)
        {
            Create<T>(entity);
        }

        public void Delete(T entity)
        {
            Delete<T>(entity);
        }

        public IEnumerable<T> Get()
        {
            return Get<T>();
        }

        public T GetByID(int id)
        {
            return GetByID<T>(id);
        }

        public IQueryable<T> Query()
        {
            return Query<T>();
        }

        public void Update(T entity)
        {
            Update<T>(entity);
        }
    }
}
