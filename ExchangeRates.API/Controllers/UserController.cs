using AutoMapper;
using ExchangeRates.Domain.Entities;
using ExchangeRates.Domain.Interfaces.Logger;
using ExchangeRates.Domain.Interfaces.Services;
using ExchangeRates.Domain.Validations;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Web.Http;

namespace ExchangeRates.API.Controllers
{
    [Route("v1/api/users")]    
    public class UserController : BaseController
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
        public async Task<IHttpActionResult> Post([FromBody] NewUserDto newUserDto)
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
        [Route("{email}")]
        public async Task<IHttpActionResult> FindByEmail(string email)
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
        public async Task<IHttpActionResult> Get()
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

        [HttpGet]
        [Route("{id:int}")]
        public async Task<IHttpActionResult> Get(int id)
        {
            try
            {
                _logger.Info($"Finding all Users");

                User user = await _userService.FindById(id);

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
    }
}
