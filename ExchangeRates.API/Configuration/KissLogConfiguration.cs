using KissLog;
using KissLog.AspNetCore;
using KissLog.CloudListeners.RequestLogsListener;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Diagnostics;
using System.Text;

namespace ExchangeRates.API.Configuration
{
   public static class KissLogConfiguration
    {
        public static void AddCustomKissLog(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            services.AddScoped((context) =>
            {
                return Logger.Factory.Get();
            });

            services.AddLogging(logging =>
            {
                logging.AddKissLog();
            });
        }

        public static void UseCustomKissLog(this IApplicationBuilder app, IConfiguration configuration)
        {
            app.UseKissLogMiddleware(options =>
            {
                ConfigureKissLog(options, configuration);
            });
        }

        private static void ConfigureKissLog(IOptionsBuilder options, IConfiguration configuration)
        {
            options.Options.AppendExceptionDetails((Exception ex) =>
            {
                StringBuilder sb = new StringBuilder();
                if (ex is NullReferenceException nullRefException)
                {
                    sb.AppendLine("Important: check for null references");
                }

                return sb.ToString();
            });

            options.InternalLog = (message) =>
            {
                Debug.WriteLine(message);
            };

            RegisterKissLogListeners(options, configuration);
        }

        private static void RegisterKissLogListeners(IOptionsBuilder options, IConfiguration configuration)
        {
            options.Listeners.Add(new RequestLogsApiListener(new KissLog.CloudListeners.Auth.Application(configuration["KissLog.OrganizationId"], configuration["KissLog.ApplicationId"]))
            {
                ApiUrl = configuration["KissLog.ApiUrl"]
            });
        }
    }
}
