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
    public class UserController : ControllerBase
    {

        [HttpPost]
        [Route("[action]")]
        public int Create(string name, string email)
        {
            throw new NotImplementedException();
        }

        [HttpGet]
        [Route("[action]/{email:email}")]
        public int Find(string email)
        {
            throw new NotImplementedException();
        }
    }
}
