using ExchangeRates.Domain.Entities;
using ExchangeRates.Domain.Interfaces.Logger;
using ExchangeRates.Domain.Interfaces.Repositories;
using ExchangeRates.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ExchangeRates.Infrastructure.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly DatabaseContext _databaseContext;
        private readonly ICustomLogger _logger;

        public UserRepository(DatabaseContext databaseContext, ICustomLogger logger)
        {
            _databaseContext = databaseContext;
            _logger = logger;
        }

        public async Task Create(User user)
        {
            _logger.Info($"Creating user on Database. User: {user?.ToString()}");

            await _databaseContext.User.AddAsync(user);
            await _databaseContext.SaveChangesAsync();

            _logger.Info($"User has been created on Database: {user?.ToString()}");
        }

        public async Task<User> GetById(int id)
        {
            _logger.Info($"Getting user on Database by id. Id: {id}");

            User user = await _databaseContext.User.FirstOrDefaultAsync(user => user.UserId == id);

            _logger.Info($"Got user on Database: {user?.ToString()}");

            return user;
        }

        public async Task<User> GetByEmail(string email)
        {
            _logger.Info($"Getting user on Database by e-mail. Email: {email}");

            User user = await _databaseContext.User.FirstOrDefaultAsync(user => user.Email == email);

            _logger.Info($"Got user on Database by e-mail: User: {user?.ToString()}");

            return user;
        }

        public async Task<IList<User>> GetAll()
        {
            _logger.Info($"Getting all users on Database.");

            IList<User> users = await _databaseContext.User.ToListAsync();

            _logger.Info($"Got all users on Database. Total: {users.Count}");
            
            return users;
        }
    }
}
