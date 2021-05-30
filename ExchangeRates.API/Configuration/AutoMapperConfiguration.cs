using ExchangeRates.API.AutoMapper;
using Microsoft.Extensions.DependencyInjection;

namespace ExchangeRates.API.Configuration
{
    public static class AutoMapperConfiguration
    {
        public static void AddCustomAutoMapper(this IServiceCollection services)
        {
            services.AddAutoMapper(typeof(AutoMapperSetup));
        }
    }
}
