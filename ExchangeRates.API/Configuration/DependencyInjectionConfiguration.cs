using ExchangeRates.Domain.Interfaces.Repositories;
using ExchangeRates.Domain.Interfaces.Services;
using ExchangeRates.Domain.Services;
using ExchangeRates.Infrastructure.Repositories;
using Microsoft.Extensions.DependencyInjection;

namespace ExchangeRates.API.Configuration
{
    public static class DependencyInjectionConfiguration
    {
        public static void AddMyDependecyInjections(this IServiceCollection services)
        {
            services.AddScoped<IUserService, UserService>();
            services.AddTransient<IUserRepository, UserRepository>();

        }
    }
}
