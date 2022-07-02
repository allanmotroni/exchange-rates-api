using ExchangeRates.Domain.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ExchangeRates.Application.Interfaces
{
    public interface IUserService
    {
        Task Create(User user);
        Task<User> FindByEmail(string email);
        Task<IList<User>> FindAll();
        Task<User> FindById(int id);
    }
}
