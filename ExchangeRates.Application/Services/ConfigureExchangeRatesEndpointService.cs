using ExchangeRates.Application.Interfaces;
using ExchangeRates.Domain.Interfaces.Logger;
using ExchangeRates.Domain.Validations;
using Microsoft.Extensions.Configuration;
using System.IO;

namespace ExchangeRates.Application.Services
{
   public class ConfigureExchangeRatesEndpointService : IConfigureEndpoint
   {
      private readonly IConfiguration _configuration;
      private readonly ICustomLogger _logger;
      private readonly ICustomValidator _customValidator;

      public ConfigureExchangeRatesEndpointService(
         IConfiguration configuration,
         ICustomLogger logger,
         ICustomValidator customValidator)
      {
         _configuration = configuration;
         _logger = logger;
         _customValidator = customValidator;         
      }

      public string Configure(string fromCurrency, string toCurrency)
      {
         string baseUrl = GetBaseURL();
         string accessKey = GetAccessKey();

         if (string.IsNullOrEmpty(accessKey)) 
            return null;

         string baseURL = ValidateBaseURL(baseUrl);
         string endpoint = CreateEndpoint(baseURL, accessKey, fromCurrency, toCurrency);

         return endpoint;
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

      private string CreateEndpoint(string baseURL, string accessKey,string fromCurrency, string toCurrency)
      {
         if (!string.IsNullOrEmpty(baseURL) && !string.IsNullOrEmpty(accessKey) && !string.IsNullOrEmpty(fromCurrency) && !string.IsNullOrEmpty(toCurrency))
         {
            return Path.Combine(baseURL, $"latest?access_key={accessKey}&base={fromCurrency}&symbols={toCurrency}");
         }

         return null;
      }

      private string GetBaseURL()
      {
         return _configuration["API:ExchangeRates:v1"];
      }

      private string GetAccessKey()
      {
         return _configuration["API:ExchangeRates:AccessKey"];
      }

   }
}
