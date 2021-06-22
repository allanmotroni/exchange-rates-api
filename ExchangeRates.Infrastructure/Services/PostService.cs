using ExchangeRates.Domain.Interfaces.Logger;
using ExchangeRates.Domain.Validations;
using ExchangeRates.Infrastructure.Interfaces;
using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace ExchangeRates.Infrastructure.Services
{
    public class PostService : IPostService
    {
        private readonly ICustomValidator _customValidator;
        private readonly ICustomLogger _logger;
        
        public PostService(ICustomValidator customValidator, ICustomLogger logger)
        {
            _customValidator = customValidator;
            _logger = logger;
        }

        public async Task<string> Post(string endpoint)
        {
            string json = null;
            try
            {
                using (HttpClient httpClient = new HttpClient())
                {
                    using (HttpResponseMessage httpResponseMessage = await httpClient.GetAsync(endpoint))
                    {
                        if (httpResponseMessage.IsSuccessStatusCode)
                        {
                            using (HttpContent httpContent = httpResponseMessage.Content)
                            {
                                json = await httpContent.ReadAsStringAsync();
                            }
                        }
                        else
                        {
                            string message = "An error on call ExchangeRatesAPI";
                            int statusCode = (int)httpResponseMessage.StatusCode;
                            _logger.Error($"{message} - endpoint: {endpoint} - statusCode: {statusCode}");
                            _customValidator.Notify("An error on call ExchangeRatesAPI.");
                        }
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
