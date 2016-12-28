using System;
using System.Linq;
using System.Threading.Tasks;
using EventAppCore.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace EventAppCore.Repositories
{
    public class RefreshTokenRepository : IRefreshTokenRepository
    {
        private readonly MainContext _mainContext;

        public RefreshTokenRepository(MainContext mainContext)
        {
            _mainContext = mainContext;
        }

        public RefreshToken GetById(string id)
        {
            throw new System.NotImplementedException();
        }

        public IQueryable<RefreshToken> GetAll()
        {
            throw new System.NotImplementedException();
        }

        public IQueryable<RefreshToken> Search(string query)
        {
            throw new NotImplementedException();
        }

        public async Task<RefreshToken> Put(RefreshToken entity)
        {
            var addedEntity = _mainContext.RefreshTokens.Add(entity).Entity;
            await _mainContext.SaveChangesAsync();
            return addedEntity;
        }

        // Use viewmodel instead?
        public async Task<RefreshToken> CreateTokenForUser(User user, string device)
        {
            return await Put(new RefreshToken()
            {
                BelongsTo = user,
                Device = device,
            });
        }

        public User GetUserFromToken(string token)
        {
            return _mainContext.RefreshTokens.Include(rt => rt.BelongsTo).Single(rt => rt.Token == token).BelongsTo;
        }
    }
}