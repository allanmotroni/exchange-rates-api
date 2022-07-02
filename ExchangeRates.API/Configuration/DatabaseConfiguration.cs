using ExchangeRates.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace ExchangeRates.API.Configuration
{
   public static class DatabaseConfiguration
   {
      public static void AddCustomDatabase(this IServiceCollection services, IConfiguration configuration)
      {
         var connection = configuration["DatabaseConnection:Sqlite"];

         services.AddDbContext<DatabaseContext>(options =>
                options.UseSqlite(connection)
            );
      }
   }
}
