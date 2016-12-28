using System.Security.Claims;
using EventAppCore.Models;
using EventAppCore.Models.View;

namespace EventAppCore.Services
{
    public interface IAccessTokenService
    {
        ViewAccessToken GetToken(User user);
        string GetIdFromToken(ClaimsPrincipal claimsPrincipal);
    }
}