using AutoMapper;
using ExchangeRates.Domain.Dto;
using ExchangeRates.Domain.Entities;
using ExchangeRates.Domain.Interfaces.Repositories;
using ExchangeRates.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;

namespace ExchangeRates.Infrastructure.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly DatabaseContext _databaseContext;
        private readonly IMapper _mapper;
        public UserRepository(DatabaseContext databaseContext, IMapper mapper)
        {
            _databaseContext = databaseContext;
            _mapper = mapper;
        }

        public async Task<UserDto> Create(NewUserDto newUserDto)
        {
            User user = _mapper.Map<User>(newUserDto);
            
            await _databaseContext.User.AddAsync(user);
            await _databaseContext.SaveChangesAsync();
            
            UserDto userDto = _mapper.Map<UserDto>(user);
            return userDto;
        }

        public async Task<UserDto> GetById(int id)
        {            
            User user = await _databaseContext.User.FirstOrDefaultAsync(user => user.UserId == id);

            UserDto userDto = _mapper.Map<UserDto>(user);
            return userDto;
        }

        public async Task<UserDto> GetByEmail(string email)
        {
            User user = await _databaseContext.User.FirstOrDefaultAsync(user => user.Email == email);

            UserDto userDto = _mapper.Map<UserDto>(user);
            return userDto;
        }

    }
}
