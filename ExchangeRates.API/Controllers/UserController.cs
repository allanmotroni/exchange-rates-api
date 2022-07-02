using AutoMapper;
using ExchangeRates.Application.Dto;
using ExchangeRates.Application.Interfaces;
using ExchangeRates.Domain.Entities;
using ExchangeRates.Domain.Interfaces.Logger;
using ExchangeRates.Domain.Validations;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ExchangeRates.API.Controllers
{
   [Route("api/v1/users")]
   public class UserController : BaseController
   {
      private readonly IMapper _mapper;

      public UserController(
         IUserService userService, 
         IMapper mapper, 
         ICustomValidator customValidator, 
         ICustomLogger logger)
          : base(customValidator, logger, userService)
      {
         _mapper = mapper;
      }

      [HttpPost]
      [SwaggerResponse(StatusCodes.Status201Created)]
      [SwaggerResponse(StatusCodes.Status400BadRequest)]
      [SwaggerResponse(StatusCodes.Status401Unauthorized)]
      public async Task<IActionResult> Post([FromBody] NewUserDto newUserDto)
      {
         try
         {
            _logger.Info($"Creating a new User: {newUserDto}");

            User user = _mapper.Map<User>(newUserDto);
            await _userService.Create(user);

            UserDto userDto = null;
            if (IsValid)
            {
               userDto = _mapper.Map<UserDto>(user);
               return CreatedAtAction(nameof(GetId), new { id = userDto.UserId }, userDto);
            }
            else
               return CustomReponse(newUserDto);
         }
         catch (Exception exception)
         {
            return CustomExceptionResponse(exception);
         }
      }

      [HttpGet]
      [Route("email/{email}")]
      [SwaggerResponse(StatusCodes.Status200OK)]
      [SwaggerResponse(StatusCodes.Status400BadRequest)]
      [SwaggerResponse(StatusCodes.Status401Unauthorized)]
      [SwaggerResponse(StatusCodes.Status404NotFound)]
      public async Task<IActionResult> FindByEmail([FromHeader] int userAuthentication, string email)
      {
         try
         {
            _logger.Info($"Finding an User by Email: {email}");

            if (!await VerifyUser(userAuthentication)) return Unauthorized();

            User user = await _userService.FindByEmail(email);
            if (user == null) return NotFound();

            UserDto userDto = _mapper.Map<UserDto>(user);

            return CustomReponse(userDto);
         }
         catch (Exception exception)
         {
            return CustomExceptionResponse(exception);
         }
      }

      [HttpGet]
      [SwaggerResponse(StatusCodes.Status200OK)]
      [SwaggerResponse(StatusCodes.Status400BadRequest)]
      [SwaggerResponse(StatusCodes.Status401Unauthorized)]
      [SwaggerResponse(StatusCodes.Status404NotFound)]
      public async Task<IActionResult> Get([FromHeader] int userAuthentication)
      {
         try
         {
            _logger.Info($"Finding all Users");

            if (!await VerifyUser(userAuthentication)) return Unauthorized();

            IList<User> users = await _userService.FindAll();
            if (users.Count == 0) return NotFound();

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
      [SwaggerResponse(StatusCodes.Status200OK)]
      [SwaggerResponse(StatusCodes.Status400BadRequest)]
      [SwaggerResponse(StatusCodes.Status401Unauthorized)]
      [SwaggerResponse(StatusCodes.Status404NotFound)]
      public async Task<IActionResult> GetId([FromHeader] int userId, int id)
      {
         try
         {
            _logger.Info($"Finding all Users");

            if (!await VerifyUser(userId)) return Unauthorized();

            User user = await _userService.FindById(id);

            if (user == null) return NotFound();

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
