using Microsoft.Extensions.DependencyInjection;

namespace ExchangeRates.API.Configuration
{
   public static class HttpClientConfiguration
   {
      public static void AddCustomHttpClients(this IServiceCollection services)
      {
         //services.AddHttpClient<IPostService, PostService>();
         //services.AddHttpClient<IGetService, GetService>();
      }
   }
}
