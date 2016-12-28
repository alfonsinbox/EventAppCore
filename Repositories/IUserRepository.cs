using System.Threading.Tasks;
using EventAppCore.Models;
using EventAppCore.Models.View;

namespace EventAppCore.Repositories
{
    public interface IUserRepository : IRepository<User>
    {
        Task<User> Put(SignUpUser userModel);
        User GetUserByCredentials(string username, string password);
    }
}