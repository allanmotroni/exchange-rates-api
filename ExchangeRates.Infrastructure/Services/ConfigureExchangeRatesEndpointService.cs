using ExchangeRates.Domain.Interfaces.Logger;
using ExchangeRates.Domain.Validations;
using ExchangeRates.Infrastructure.Interfaces;
using Microsoft.Extensions.Configuration;
using System.IO;

namespace ExchangeRates.Infrastructure.Services
{
    public class ConfigureExchangeRatesEndpointService : IConfigureEndpoint
    {
        private readonly IConfiguration _configuration;
        private readonly ICustomLogger _logger;
        private readonly ICustomValidator _customValidator;
        public ConfigureExchangeRatesEndpointService(IConfiguration configuration, ICustomLogger logger, ICustomValidator customValidator)
        {
            _configuration = configuration;
            _logger = logger;
            _customValidator = customValidator;
        }

        public string Configure(params string[] parameters)
        {
            string fromCurrency = parameters[0];
            string toCurrency = parameters[1];

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
            if (!string.IsNullOrEmpty(baseURL) && !string.IsNullOrEmpty(fromCurrency) && !string.IsNullOrEmpty(toCurrency))
            {
                string token = GetAccessKey();
                return Path.Combine(baseURL, $"latest?access_key={token}&base={fromCurrency}&symbols={toCurrency}");
            }

            return null;
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
