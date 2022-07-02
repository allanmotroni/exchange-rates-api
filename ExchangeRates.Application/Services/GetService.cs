using ExchangeRates.Application.Interfaces;
using ExchangeRates.Domain.Interfaces.Logger;
using ExchangeRates.Domain.Validations;
using RestSharp;
using System;
using System.Threading.Tasks;

namespace ExchangeRates.Application.Services
{
   public class GetService : IGetService
   {
      private readonly ICustomValidator _customValidator;
      private readonly ICustomLogger _logger;

      public GetService(
         ICustomValidator customValidator,
         ICustomLogger logger)
      {
         _customValidator = customValidator;
         _logger = logger;
      }

      //public async Task<string> GetAsync(string endpoint)
      //{
      //   string json = null;
      //   try
      //   {
      //      using (HttpClient httpClient = new HttpClient())
      //      {
      //         using (HttpResponseMessage httpResponseMessage = await httpClient.GetAsync(endpoint))
      //         {
      //            if (httpResponseMessage.IsSuccessStatusCode)
      //            {
      //               using (HttpContent httpContent = httpResponseMessage.Content)
      //               {
      //                  json = await httpContent.ReadAsStringAsync();
      //               }
      //            }
      //            else
      //            {
      //               string message = "An error on call API";
      //               int statusCode = (int)httpResponseMessage.StatusCode;
      //               _logger.Error($"{message} - endpoint: {endpoint} - statusCode: {statusCode}");
      //               _customValidator.Notify("An error on call API.");
      //            }
      //         }
      //      }
      //   }
      //   catch (Exception ex)
      //   {
      //      _customValidator.Notify(ex.Message);
      //   }

      //   return json;
      //}

      public async Task<string> GetAsync(string endpoint)
      {
         string json = null;
         try
         {
            using (var client = new RestClient(endpoint))
            {
               var request = new RestRequest();
               RestResponse response = await client.GetAsync(request);
               if (response.IsSuccessful)
               {
                  json = response.Content;
               }
               else
               {
                  string message = "An error on call API";
                  int statusCode = (int)response.StatusCode;
                  _logger.Error($"{message} - endpoint: {endpoint} - statusCode: {statusCode}");
                  _customValidator.Notify("An error on call API.");
               }
            }
         }
         catch (Exception ex)
         {
            _customValidator.Notify(ex.Message);
         }

         return json;
      }
   }
}
