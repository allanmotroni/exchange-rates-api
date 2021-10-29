using System;

namespace ExchangeRates.Domain.Interfaces.Logger
{
    public interface ICustomLogger
    {
        void Info(string message);

        void Warn(string message);

        void Error(string message);

        void Exception(string message, Exception exception);

        void Exception(Exception exception);

    }
}
