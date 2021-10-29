using ExchangeRates.API.Interfaces;
using ExchangeRates.Domain.Entities;
using ExchangeRates.Domain.Interfaces.Logger;
using ExchangeRates.Domain.Interfaces.Services;
using ExchangeRates.Domain.Validations;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace ExchangeRates.API.Controllers
{
    [ApiController]
    public abstract class BaseController : ControllerBase
    {
        protected readonly ICustomValidator _customValidator;
        protected readonly ICustomLogger _logger;
        private readonly IUserService _userService;
        protected BaseController(ICustomValidator customValidator, ICustomLogger logger, IUserService userService)
        {
            _customValidator = customValidator;
            _logger = logger;
            _userService = userService;
        }

        protected IActionResult CustomReponse<T>(T returnedObject)
        {
            if (IsValid)
                return CustomSuccessResponse(returnedObject);
            else
                return CustomBadRequestResponse();
        }

        protected async Task<bool> VerifyUser(IUser userAuthentication)
        {
            User user = await _userService.FindById(userAuthentication.UserId);
            return user != null;
        }

        protected async Task<bool> VerifyUser(int userId)
        {
            return await VerifyUser(new UserDto { UserId = userId });
        }

        private IActionResult CustomBadRequestResponse()
        {
            _logger.Warn($"{_customValidator.GetStringValidations()}");

            return BadRequest();
        }

        protected IActionResult CustomExceptionResponse(Exception ex)
        {
            _logger.Exception(ex);

            return BadRequest();
        }

        private IActionResult CustomSuccessResponse<T>(T returnedObject)
        {
            _logger.Info($"Success response. {returnedObject}");

            return Ok(new
            {
                success = true,
                data = returnedObject
            });
        }

        protected bool IsValid
        {
            get
            {
                return !_customValidator.HasErrors();
            }
        }
    }
}
