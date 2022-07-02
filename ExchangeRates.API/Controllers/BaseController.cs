using ExchangeRates.Application.Dto;
using ExchangeRates.Application.Interfaces;
using ExchangeRates.Domain.Entities;
using ExchangeRates.Domain.Interfaces.Logger;
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
      protected readonly IUserService _userService;

      protected BaseController(
         ICustomValidator customValidator, 
         ICustomLogger logger, 
         IUserService userService)
      {
         _customValidator = customValidator;
         _logger = logger;
         _userService = userService;
      }

      protected IActionResult CustomReponse<T>(T returnedObject)
      {
         if (IsValid)
            return CustomSuccessResponse(returnedObject);

         if (returnedObject == null)
            return CustomNotFoundResponse();

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

      private IActionResult CustomNotFoundResponse()
      {
         var message = _customValidator.GetStringValidations();

         _logger.Warn(message);

         return NotFound(message);
      }

      private IActionResult CustomBadRequestResponse()
      {
         var message = _customValidator.GetStringValidations();

         _logger.Warn(message);

         return BadRequest(message);
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
