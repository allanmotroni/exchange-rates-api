using ExchangeRates.Domain.Dto;
using ExchangeRates.Domain.Interfaces.Repositories;
using ExchangeRates.Domain.Interfaces.Services;
using System.Threading.Tasks;

namespace ExchangeRates.Domain.Services
{
    public class UserService: IUserService
    {
        private readonly IUserRepository _userRepository;
        public UserService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<UserDto> Create(NewUserDto newUserDto)
        {
            UserDto userDto = await _userRepository.Create(newUserDto);
            return userDto;
        }

        public async Task<UserDto> FindByEmail(string email)
        {
            ValidateEmail(email);

            UserDto userDto = await _userRepository.GetByEmail(email);
            
            return userDto;
        }

        private bool ValidateEmail(string email)
        {
            return true;
        }
    }
}
