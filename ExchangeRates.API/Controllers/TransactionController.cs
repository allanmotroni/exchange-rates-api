using ExchangeRates.Domain.API;
using ExchangeRates.Domain.Interfaces.Logger;
using ExchangeRates.Domain.Validations;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace ExchangeRates.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TransactionController : MyControllerBase
    {
        protected TransactionController(ICustomValidator customValidator, ICustomLogger logger)
            : base(customValidator, logger) { }

        [HttpPost]
        [Route("[action]")]
        public IActionResult Convert([FromBody] NewTransactionDto newTransaction)
        {
            return CustomReponse(new TransactionDto());
        }

        [HttpGet]
        [Route("[action]/{userId:int}")]
        public IActionResult ListByUserId(int userId)
        {
            return CustomReponse<IEnumerable<TransactionDto>>(new List<TransactionDto>());
        }
    }
}
