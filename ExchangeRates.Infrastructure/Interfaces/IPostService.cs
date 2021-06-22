using System.Threading.Tasks;

namespace ExchangeRates.Infrastructure.Interfaces
{
    public interface IPostService
    {
        public Task<string> Post(string endpoint);
    }
}
