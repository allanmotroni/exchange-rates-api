using ExchangeRates.Domain.Interfaces.Logger;
using Microsoft.Extensions.Logging;
using System;

namespace ExchangeRates.Infrastructure.Logger
{

    public class CustomLogger : ICustomLogger
    {
        private readonly ILogger<CustomLogger> _logger;
        public CustomLogger(ILogger<CustomLogger> logger)
        {
            _logger = logger;
        }

        public void Info(string message)
        {
            _logger.LogInformation($"{BaseInformation()} - {message}");
        }

        public void Warn(string message)
        {
            _logger.LogWarning($"{BaseInformation()} - {message}");
        }

        public void Error(string message)
        {
            _logger.LogError($"{BaseInformation()} - {message}");
        }

        public void Exception(Exception exception)
        {
            _logger.LogCritical(exception, $"{BaseInformation()}");
        }

        public void Exception(string message, Exception exception)
        {
            _logger.LogCritical(exception, $"{BaseInformation()} - {message}");
        }

        private string BaseInformation()
        {
            string method = (new System.Diagnostics.StackTrace()).GetFrame(1).GetMethod().Name;

            return $"Date: {DateTime.UtcNow.ToString("dd/MM/yyyy HH:mm:ss")} - Method: {method}";
        }
    }
}
