using System.Threading.Tasks;

namespace ExchangeRates.Domain.Interfaces.Repositories
{
    public interface IExchangeRateRepository
    {
        public Task<double> GetEuroExchangeRate(string fromCurrency, string toCurrency);
    }
}
