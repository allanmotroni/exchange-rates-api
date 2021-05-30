using ExchangeRates.Domain.Dto;
using ExchangeRates.Domain.Interfaces.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;

namespace ExchangeRates.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : MyControllerBase
    {
        protected readonly ILogger _logger;
        protected readonly IUserService _userService;
        public UserController(ILogger<MyControllerBase> logger, IUserService userService)
        {
            _logger = logger;
            _userService = userService;
        }

        [HttpPost]
        [Route("[action]")]
        public async Task<IActionResult> Create([FromBody]NewUserDto newUserDto)
        {
            _logger.LogInformation($"Creating a new User: {newUserDto}");

            UserDto userDto =await _userService.Create(newUserDto);

            return CustomReponse(userDto);
        }

        [HttpGet]
        [Route("[action]/{email}")]
        public async Task<IActionResult> FindByEmail(string email)
        {
            _logger.LogInformation($"Finding an User by Email: {email}");

            UserDto userDto = await _userService.FindByEmail(email);

            return CustomReponse(userDto);
        }
    }
}
