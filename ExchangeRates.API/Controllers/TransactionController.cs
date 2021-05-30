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
    public class TransactionController : ControllerBase
    {
        [HttpPost]
        [Route("[action]")]
        public void Convert()
        {
            throw new NotImplementedException();
        }

        [HttpGet]
        [Route("[action]/{userId}:int")]
        public void ListByUserId(int userId)
        {
            throw new NotImplementedException();
        }
    }
}
