using ExchangeRates.Domain.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExchangeRates.Domain.Interfaces.Services
{
    public interface IUserService
    {
        public Task<UserDto> Create(NewUserDto newUserDto);

        public Task<UserDto> FindByEmail(string email);
        
    }
}
