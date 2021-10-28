using ExchangeRates.Domain.Interfaces.Logger;
using ExchangeRates.Domain.Validations;
using System;
using System.Web.Http;

namespace ExchangeRates.API.Controllers
{
    public abstract class BaseController : ApiController
    {
        protected readonly ICustomValidator _customValidator;
        protected readonly ICustomLogger _logger;
        protected BaseController(ICustomValidator customValidator, ICustomLogger logger)
        {
            _customValidator = customValidator;
            _logger = logger;
        }

        public IHttpActionResult CustomReponse<T>(T returnedObject)
        {
            if (IsValid)
                return CustomSuccessResponse(returnedObject);
            else
                return CustomBadRequestResponse();
        }

        private IHttpActionResult CustomBadRequestResponse()
        {
            _logger.Warn($"{_customValidator.GetStringValidations()}");

            return BadRequest();
        }

        protected IHttpActionResult CustomExceptionResponse(Exception ex)
        {
            _logger.Exception(ex);

            return BadRequest();
        }

        private IHttpActionResult CustomSuccessResponse<T>(T returnedObject)
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
