using ExchangeRates.Domain.Interfaces.Logger;
using ExchangeRates.Domain.Interfaces.Repositories;
using ExchangeRates.Domain.Interfaces.Services;
using ExchangeRates.Domain.Services;
using ExchangeRates.Domain.Validations;
using ExchangeRates.Infrastructure.Logger;
using ExchangeRates.Infrastructure.Repositories;
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

            services.AddScoped<ICustomValidator, Validator>();

            services.AddScoped<ValidationService, ValidationService>();

        }
    }
}
