using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using EventAppCore.Models;
using EventAppCore.Models.View;
using Microsoft.EntityFrameworkCore;

namespace EventAppCore.Repositories
{
    public class EventRepository : IEventRepository
    {
        private readonly MainContext _mainContext;

        public EventRepository(MainContext mainContext)
        {
            _mainContext = mainContext;
        }

        public Event GetById(string id)
        {
            return _mainContext.Events.Single(e => e.Id == id);
        }

        public IQueryable<Event> GetAll()
        {
            return _mainContext.Events
                .Include(e => e.CreatedBy)
                .Include(e => e.UserEvents)
                .Include(e => e.Location)
                .AsQueryable();
        }

        public IQueryable<Event> Search(string query)
        {
            return GetAll().Where(e => e.Name.ToLower().Contains(query)).AsQueryable();
        }

        public async Task<Event> Put(Event entity)
        {
            var addedEntity = _mainContext.Events.Add(entity).Entity;
            _mainContext.UserEvents.Add(new UserEvent()
            {
                Event = addedEntity,
                User = addedEntity.CreatedBy,
                Role = 123,
                Note = "I just want this to work"
            });
            await _mainContext.SaveChangesAsync();
            return addedEntity;
        }

        public async Task<Event> Put(CreateEvent model, User creator, Location location)
        {
            var eventEntity = Mapper.Map<Event>(model);
            eventEntity.CreatedBy = creator;
            eventEntity.Location = location;
            return await Put(eventEntity);
        }
    }
}