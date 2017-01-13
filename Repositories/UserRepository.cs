using System;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using EventAppCore.Models;
using EventAppCore.Models.View;
using EventAppCore.Services;
using Microsoft.EntityFrameworkCore;

namespace EventAppCore.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly MainContext _mainContext;
        private readonly EncryptionService _encryptionService;

        public UserRepository(MainContext mainContext, EncryptionService encryptionService)
        {
            _mainContext = mainContext;
            _encryptionService = encryptionService;
        }

        public User GetById(string id)
        {
            return _mainContext.Users.Single(u => u.Id == id);
        }

        public IQueryable<User> GetAll()
        {
            return _mainContext.Users
                .Include(u => u.UserEvents)
                    .ThenInclude(ue => ue.Event)
                .Include(u => u.RefreshTokens)
                //.Include(u => u.Interests)
                .AsQueryable();
        }

        public IQueryable<User> Search(string query)
        {
            query = query.ToLower();
            return GetAll()
                .Where(u => u.Username.ToLower().Contains(query) ||
                            u.LastName.ToLower().Contains(query) ||
                            u.FirstName.ToLower().Contains(query))
                .AsQueryable();
        }

        public async Task<User> Put(User user)
        {
            var addedEntity = _mainContext.Users.Add(user).Entity;
            await _mainContext.SaveChangesAsync();
            return addedEntity;
        }

        public async Task<User> Put(SignUpUser userModel)
        {
            var userEntity = Mapper.Map<User>(userModel);
            userEntity.SaltAndHash = _encryptionService.CreatePasswordSaltHash(userModel.Password);
            return await Put(userEntity);
        }

        public User GetUserByCredentials(string username, string password)
        {
            var user = _mainContext.Users.Single(u => u.Username == username);
            return _encryptionService.IsPasswordValid(password, user.SaltAndHash) ? user : null;
            //_mainContext.UserEvents.ProjectTo<>()
            //(new ProjectionExpression()).To
        }
    }
}