using ExchangeRates.Application.Interfaces;
using ExchangeRates.Application.Services;
using ExchangeRates.Domain.Interfaces.Logger;
using ExchangeRates.Domain.Interfaces.Repositories;
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
         services.AddScoped<ICustomValidator, CustomValidator>();
         services.AddTransient<IConfigureEndpoint, ConfigureExchangeRatesEndpointService>();
         
         // Services
         services.AddScoped<IUserService, UserService>();
         services.AddScoped<IExchangeTransactionService, ExchangeTransactionService>();
         services.AddScoped<ISymbolService, SymbolService>();
         services.AddScoped<ValidationService, ValidationService>();
         services.AddTransient<IGetService, GetService>();
         
         // Repositories
         services.AddTransient<IUserRepository, UserRepository>();
         services.AddTransient<ITransactionRepository, TransactionRepository>();
         services.AddTransient<IExchangeRateService, ExchangeRateService>();

      }
   }
}
