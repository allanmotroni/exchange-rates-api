using ExchangeRates.Application.Dto;
using ExchangeRates.Application.Interfaces;
using ExchangeRates.Common.Extension;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ExchangeRates.Application.Services
{
   public class ExchangeRateService : IExchangeRateService
   {
      private readonly IGetService _getService;
      private readonly IConfigureEndpoint _configureEndpoint;

      public ExchangeRateService(           
           IGetService getService, 
           IConfigureEndpoint configureEndpoint)
      {
         _getService = getService;
         _configureEndpoint = configureEndpoint;
      }

      public async Task<double> GetExchangeRate(string fromCurrency, string toCurrency)
      {
         double rate = 0;
         string endpoint = _configureEndpoint.Configure(fromCurrency, toCurrency);
         string json = await _getService.GetAsync(endpoint);

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
