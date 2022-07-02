using System.Threading.Tasks;

namespace ExchangeRates.Application.Interfaces
{
    public interface IExchangeRateService
    {
        Task<double> GetExchangeRate(string fromCurrency, string toCurrency);
    }
}
