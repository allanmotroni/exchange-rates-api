using AutoMapper;
using ExchangeRates.Domain.API;
using ExchangeRates.Domain.Entities;
using ExchangeRates.Domain.Interfaces.Logger;
using ExchangeRates.Domain.Interfaces.Services;
using ExchangeRates.Domain.Validations;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ExchangeRates.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : MyControllerBase
    {
        private readonly IUserService _userService;
        private readonly IMapper _mapper;

        public UserController(IUserService userService, IMapper mapper, ICustomValidator customValidator, ICustomLogger logger)
            : base(customValidator, logger)
        {
            _userService = userService;
            _mapper = mapper;
        }

        [HttpPost]
        [Route("[action]")]
        public async Task<IActionResult> Create([FromBody] NewUserDto newUserDto)
        {
            try
            {
                _logger.Info($"Creating a new User: {newUserDto}");

                User user = _mapper.Map<User>(newUserDto);
                await _userService.Create(user);

                UserDto userDto = null;
                if (IsValid)
                    userDto = _mapper.Map<UserDto>(user);

                return CustomReponse(userDto);
            }
            catch (Exception exception)
            {
                return CustomExceptionResponse(exception);
            }
        }

        [HttpGet]
        [Route("[action]/{email}")]
        public async Task<IActionResult> FindByEmail(string email)
        {
            try
            {
                _logger.Info($"Finding an User by Email: {email}");

                User user = await _userService.FindByEmail(email);

                UserDto userDto = null;
                if (IsValid)
                    userDto = _mapper.Map<UserDto>(user);

                return CustomReponse(userDto);
            }
            catch (Exception exception)
            {
                return CustomExceptionResponse(exception);
            }
        }

        [HttpGet]
        [Route("[action]")]
        public async Task<IActionResult> FindAll()
        {
            try
            {
                _logger.Info($"Finding all Users");

                IEnumerable<User> users = await _userService.FindAll();

                IEnumerable<UserDto> usersDto = null;
                if (IsValid)
                    usersDto = _mapper.Map<IEnumerable<UserDto>>(users);

                return CustomReponse(usersDto);
            }
            catch (Exception exception)
            {
                return CustomExceptionResponse(exception);
            }
        }
    }
}
