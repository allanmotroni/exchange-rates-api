using ExchangeRates.Domain.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ExchangeRates.Domain.Interfaces.Repositories
{
    public interface IUserRepository
    {
        Task Create(User user);

        Task<User> GetById(int id);

        Task<User> GetByEmail(string email);

        Task<IList<User>> GetAll();
    }
}
