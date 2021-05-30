using ExchangeRates.Domain.Dto;
using ExchangeRates.Domain.DTO;
using ExchangeRates.Domain.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace ExchangeRates.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : MyControllerBase
    {
        protected readonly ILogger _logger;
        public UserController(ILogger<MyControllerBase> logger)
        {
            _logger = logger;
        }

        [HttpPost]
        [Route("[action]")]
        public IActionResult Create([FromBody]NewUserDto newUser)
        {
            _logger.LogInformation($"Creating a new User: {newUser}");

            return CustomReponse(new UserDto());
        }

        [HttpGet]
        [Route("[action]/{email}")]
        public IActionResult Find(string email)
        {
            _logger.LogInformation($"Finding an User by Email param: {email}");

            return CustomReponse(new UserDto());
        }
    }
}
