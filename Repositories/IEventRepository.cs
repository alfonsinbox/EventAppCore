using System.Threading.Tasks;
using EventAppCore.Models;
using EventAppCore.Models.View;

namespace EventAppCore.Repositories
{
    public interface IEventRepository : IRepository<Event>
    {
        Task<Event> Put(CreateEvent model, User creator, Location location);
    }
}