using ExchangeRates.Domain.Entities;
using ExchangeRates.Domain.Interfaces.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ExchangeRates.Test.Infrastructure.Repositories
{
    public class UserRepositoryFake : IUserRepository
    {
        private IList<User> users;
        private int idCounter;

        public UserRepositoryFake()
        {
            users = new List<User>();
        }

        private int GetNextUserId()
        {
            return ++idCounter;
        }

        private IList<User> LoadFakeUsers()
        {
            users = new List<User>()
            {
                new User{ UserId = GetNextUserId(), Active = true, CreatedAt = DateTime.Now, Email = $"email{idCounter}@email.com", Name = "User "+ idCounter },
                new User{ UserId = GetNextUserId(), Active = true, CreatedAt = DateTime.Now, Email = $"email{idCounter}@email.com", Name = "User "+ idCounter },
                new User{ UserId = GetNextUserId(), Active = true, CreatedAt = DateTime.Now, Email = $"email{idCounter}@email.com", Name = "User "+ idCounter },
                new User{ UserId = GetNextUserId(), Active = true, CreatedAt = DateTime.Now, Email = $"email{idCounter}@email.com", Name = "User "+ idCounter }
            };

            return users;
        }

        public async Task Create(User user)
        {
            user.UserId = GetNextUserId();
            users.Add(user);

            await Task.FromResult(user);
        }

        public Task<IList<User>> GetAll()
        {
            IList<User> users = LoadFakeUsers();
            return Task.FromResult(users);
        }

        public Task<User> GetByEmail(string email)
        {
            User user = users.FirstOrDefault(p => p.Email == email);

            return Task.FromResult(user);
        }

        public Task<User> GetById(int id)
        {
            User user = users.FirstOrDefault(p => p.UserId == id);
            
            return Task.FromResult(user);
        }
    }
}
