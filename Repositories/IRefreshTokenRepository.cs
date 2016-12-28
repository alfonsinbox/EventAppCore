using System.Threading.Tasks;
using EventAppCore.Models;

namespace EventAppCore.Repositories
{
    public interface IRefreshTokenRepository : IRepository<RefreshToken>
    {
        Task<RefreshToken> CreateTokenForUser(User user, string device);
        User GetUserFromToken(string token);
    }
}