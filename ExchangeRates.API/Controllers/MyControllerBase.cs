using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace ExchangeRates.API.Controllers
{
    public class MyControllerBase : ControllerBase
    {        
        public IActionResult CustomReponse<T>(T returnedObject)
        {
            try
            {
                if (IsValid())
                    return CustomSuccessResponse(returnedObject);
                else
                    return CustomBadRequestResponse();
            }
            catch (Exception ex)
            {
                return CustomExceptionResponse();
            }
        }

        private IActionResult CustomBadRequestResponse()
        {
            return BadRequest(new
            {
                success = false,
                message = ""
            });
        }

        private IActionResult CustomExceptionResponse()
        {

            return BadRequest(new
            {
                success = false,
                message = "Please try again later."
            });
        }

        private IActionResult CustomSuccessResponse<T>(T returnedObject)
        {            
            return Ok(new
            {
                success = true,
                data = returnedObject
            });
        }

        private bool IsValid()
        {
            return true;
        }
    }
}
