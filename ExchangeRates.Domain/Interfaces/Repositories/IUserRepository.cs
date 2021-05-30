using ExchangeRates.Domain.Dto;
using System.Threading.Tasks;

namespace ExchangeRates.Domain.Interfaces.Repositories
{
    public interface IUserRepository
    {
        public Task<UserDto> Create(NewUserDto newUserDto);

        public Task<UserDto> GetById(int id);

        public Task<UserDto> GetByEmail(string email);
    }
}
