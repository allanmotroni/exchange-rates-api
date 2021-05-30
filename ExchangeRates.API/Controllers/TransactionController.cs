using ExchangeRates.Domain.Dto;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ExchangeRates.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TransactionController : MyControllerBase
    {
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
