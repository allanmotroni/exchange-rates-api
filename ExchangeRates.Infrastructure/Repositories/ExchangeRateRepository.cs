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
        public ExchangeRateRepository(ICustomValidator customValidator, ICustomLogger logger, IConfiguration configuration, IPostService postService)
        {
            _customValidator = customValidator;
            _logger = logger;
            _configuration = configuration;
            _postService = postService;
        }

        public async Task<double> GetExchangeRate(string fromCurrency, string toCurrency)
        {
            double rate = 0;
            string endpoint = ConfigureEndpoint(fromCurrency, toCurrency);
            string json = await _postService.Post(endpoint);

            ExchangeRatesDto exchangeRatesDTO = json.ToClassOf<ExchangeRatesDto>();
            if (exchangeRatesDTO != null)            
            {
                KeyValuePair<string, double> keyValuePairRate = exchangeRatesDTO.Rates.FirstOrDefault(p => p.Key == toCurrency);
                rate = keyValuePairRate.Value;
            }

            return rate;
        }

        private string ConfigureEndpoint(string fromCurrency, string toCurrency)
        {
            string baseURL = ValidateBaseURL(GetBaseURL());
            string endpoint = CreateEndpoint(baseURL, fromCurrency, toCurrency);

            return endpoint;
        }

        private string GetBaseURL()
        {
            return GetConfigurationByKey("API:ExchangeRates:v1");
        }

        private string ValidateBaseURL(string url)
        {
            if (string.IsNullOrEmpty(url))
            {
                string message = "API ExchangeRates url is empty.";
                _logger.Error(message);
                _customValidator.Notify(message);
            }

            return url;
        }

        private string CreateEndpoint(string baseURL, string fromCurrency, string toCurrency)
        {
            string token = GetAccessKey();
            return Path.Combine(baseURL, $"latest?access_key={token}&base={fromCurrency}&symbols={toCurrency}");
        }

        private string GetAccessKey()
        {
            return GetConfigurationByKey("API:ExchangeRates:AccessKey");
        }

        private string GetConfigurationByKey(string key)
        {
            return _configuration[key];
        }               
    }
}
