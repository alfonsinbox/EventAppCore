using System.Threading.Tasks;
using EventAppCore.Models;
using EventAppCore.Models.View;

namespace EventAppCore.Repositories
{
    public interface ILocationRepository : IRepository<Location>
    {
        Task<Location> Put(CreateLocation locationModel, User creator);
    }
}