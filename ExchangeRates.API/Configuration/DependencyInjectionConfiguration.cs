using ExchangeRates.Domain.Interfaces.Logger;
using ExchangeRates.Domain.Interfaces.Repositories;
using ExchangeRates.Domain.Interfaces.Services;
using ExchangeRates.Domain.Services;
using ExchangeRates.Domain.Validations;
using ExchangeRates.Infrastructure.Interfaces;
using ExchangeRates.Infrastructure.Logger;
using ExchangeRates.Infrastructure.Repositories;
using ExchangeRates.Infrastructure.Services;
using Microsoft.Extensions.DependencyInjection;

namespace ExchangeRates.API.Configuration
{
    public static class DependencyInjectionConfiguration
    {
        public static void AddCustomDependecyInjections(this IServiceCollection services)
        {
            services.AddScoped<ICustomLogger, CustomLogger>();

            services.AddScoped<IUserService, UserService>();

            services.AddScoped<IExchangeTransactionService, ExchangeTransactionService>();

            services.AddTransient<IUserRepository, UserRepository>();

            services.AddTransient<ITransactionRepository, TransactionRepository>();

            services.AddTransient<IExchangeRateRepository, ExchangeRateRepository>();

            services.AddScoped<ICustomValidator, CustomValidator>();

            services.AddScoped<ValidationService, ValidationService>();
            
            services.AddTransient<IPostService, PostService>();

            services.AddTransient<IConfigureEndpoint, ConfigureExchangeRatesEndpointService>();

        }
    }
}
