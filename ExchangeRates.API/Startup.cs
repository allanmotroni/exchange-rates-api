using ExchangeRates.API.Configuration;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace ExchangeRates.API
{
   public class Startup
   {
      public Startup(IConfiguration configuration)
      {
         Configuration = configuration;
      }

      public IConfiguration Configuration { get; }

      public void ConfigureServices(IServiceCollection services)
      {
         services.AddCustomSwagger();

         services.AddCustomDependecyInjections();

         //services.AddCustomHttpClients();

         services.AddCustomDatabase(Configuration);

         services.AddCustomAutoMapper();

         services.AddControllers();

         services.AddCustomKissLog(Configuration);

         services.AddHealthChecks();
      }

      public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
      {
         if (env.IsDevelopment())
         {
            app.UseDeveloperExceptionPage();
         }

         app.UseHealthChecks("/healthcheck");

         app.UseRouting();

         app.UseAuthorization();

         app.UseCustomKissLog(Configuration);

         app.UseEndpoints(endpoints =>
         {
            endpoints.MapControllers();
         });

         app.UseCustomSwagger();
      }
   }
}
