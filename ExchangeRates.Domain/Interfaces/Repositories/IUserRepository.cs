using ExchangeRates.Domain.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ExchangeRates.Domain.Interfaces.Repositories
{
    public interface IUserRepository
    {
        public Task<User> Create(User user);

        public Task<User> GetById(int id);

        public Task<User> GetByEmail(string email);

        public Task<IList<User>> GetAll();
    }
}
