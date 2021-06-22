using ExchangeRates.Domain.Interfaces.Logger;
using ExchangeRates.Domain.Interfaces.Repositories;
using ExchangeRates.Domain.Validations;
using ExchangeRates.Infrastructure.Dto;
using ExchangeRates.Infrastructure.Extension;
using ExchangeRates.Infrastructure.Interfaces;
using Microsoft.Extensions.Configuration;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace ExchangeRates.Infrastructure.Repositories
{
    public class ExchangeRateRepository : IExchangeRateRepository
    {
        private readonly ICustomValidator _customValidator;
        private readonly ICustomLogger _logger;
        private readonly IConfiguration _configuration;
        private readonly IPostService _postService;
        private readonly IConfigureEndpoint _configureEndpoint;
        public ExchangeRateRepository(ICustomValidator customValidator, ICustomLogger logger, IConfiguration configuration, IPostService postService, IConfigureEndpoint configureEndpoint)
        {
            _customValidator = customValidator;
            _logger = logger;
            _configuration = configuration;
            _postService = postService;
            _configureEndpoint = configureEndpoint;
        }

        public async Task<double> GetExchangeRate(string fromCurrency, string toCurrency)
        {
            double rate = 0;
            string endpoint = _configureEndpoint.Configure(fromCurrency, toCurrency);
            string json = await _postService.Post(endpoint);

            ExchangeRatesDto exchangeRatesDTO = json.ToClassOf<ExchangeRatesDto>();
            if (exchangeRatesDTO != null)
            {
                KeyValuePair<string, double> keyValuePairRate = exchangeRatesDTO.Rates.FirstOrDefault(p => p.Key == toCurrency);
                rate = keyValuePairRate.Value;
            }

            return rate;
        }               
    }
}
