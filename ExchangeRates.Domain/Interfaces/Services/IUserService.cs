using ExchangeRates.Domain.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ExchangeRates.Domain.Interfaces.Services
{
    public interface IUserService
    {
        public Task Create(User user);

        public Task<User> FindByEmail(string email);

        public Task<IEnumerable<User>> FindAll();

    }
}
