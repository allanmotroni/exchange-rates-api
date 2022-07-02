using ExchangeRates.Application.Interfaces;
using ExchangeRates.Domain.Entities;
using ExchangeRates.Domain.Interfaces.Logger;
using Microsoft.Extensions.Configuration;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ExchangeRates.Application.Services
{
   public class SymbolService : ISymbolService
   {
      private readonly ICustomLogger _logger;
      private readonly IConfiguration _configuration;
      private readonly IGetService _getService;

      public SymbolService(
         ICustomLogger logger,
         IConfiguration configuration,
         IGetService getService)
      {
         _logger = logger;
         _configuration = configuration;
         _getService = getService;
      }

      public async Task<IList<Symbol>> GetAll()
      {
         string baseUrl = _configuration["API:ExchangeRates:v1"];
         string accessKey = _configuration["API:ExchangeRates:AccessKey"];

         //string json = await _getService.GetAsync(endpoint, accessKey);

         //IList<SymbolDto> listDto = json.ToClassOf<IList<SymbolDto>>();
         return null;
      }
   }
}
