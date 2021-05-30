using System;

namespace ExchangeRates.Domain.Interfaces.Logger
{
    public interface ICustomLogger
    {
        public void Info(string message);

        public void Warn(string message);

        public void Error(string message);

        public void Exception(string message, Exception exception);

        public void Exception(Exception exception);

    }
}
