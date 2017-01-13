using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using EventAppCore.Models;
using EventAppCore.Models.View;
using Microsoft.EntityFrameworkCore;

namespace EventAppCore.Repositories
{
    public class LocationRepository : ILocationRepository
    {
        private readonly MainContext _mainContext;

        public LocationRepository(MainContext mainContext)
        {
            _mainContext = mainContext;
        }

        public Location GetById(string id)
        {
            return _mainContext.Locations.Single(l => l.Id == id);
        }

        public IQueryable<Location> GetAll()
        {
            return _mainContext.Locations
                .Include(l => l.CreatedBy)
                .Include(l => l.Events)
                .AsQueryable();
        }

        public IQueryable<Location> Search(string query)
        {
            return GetAll()
                .Where(l => l.Name.ToLower().Contains(query) ||
                            l.Address.ToLower().Contains(query))
                .AsQueryable();
        }

        public async Task<Location> Put(Location entity)
        {
            var addedEntity = _mainContext.Locations.Add(entity).Entity;
            await _mainContext.SaveChangesAsync();
            return addedEntity;
        }

        public async Task<Location> Put(CreateLocation locationModel, User creator)
        {
            var locationEntity = Mapper.Map<Location>(locationModel);
            locationEntity.CreatedBy = creator;
            return await Put(locationEntity);
        }
    }
}