using ExchangeRates.Domain.Interfaces.Logger;
using ExchangeRates.Domain.Validations;
using Microsoft.AspNetCore.Mvc;
using System;

namespace ExchangeRates.API.Controllers
{
    public abstract class MyControllerBase : ControllerBase
    {
        protected readonly ICustomValidator _customValidator;
        protected readonly ICustomLogger _logger;
        protected MyControllerBase(ICustomValidator customValidator, ICustomLogger logger)
        {
            _customValidator = customValidator;
            _logger = logger;
        }

        public IActionResult CustomReponse<T>(T returnedObject)
        {
            if (IsValid)
                return CustomSuccessResponse(returnedObject);
            else
                return CustomBadRequestResponse();
        }

        private IActionResult CustomBadRequestResponse()
        {
            _logger.Warn($"{_customValidator.GetStringValidations()}");

            return BadRequest(new
            {
                success = false,
                messages = _customValidator.GetValidations()
            });
        }

        protected IActionResult CustomExceptionResponse(Exception ex)
        {
            _logger.Exception(ex);

            return BadRequest(new
            {
                success = false,
                message = "An error has occured while trying to proccess this request."
            });
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

        public bool IsValid
        {
            get
            {
                return !_customValidator.HasErrors();
            }
        }
    }
}
