using System.Threading.Tasks;

namespace ExchangeRates.Domain.Interfaces.Repositories
{
    public interface IExchangeRateRepository
    {
        public Task<double> GetExchangeRate(string fromCurrency, string toCurrency);
    }
}
