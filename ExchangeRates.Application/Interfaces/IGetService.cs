using System.Threading.Tasks;

namespace ExchangeRates.Application.Interfaces
{
   public interface IGetService
   {
      Task<string> GetAsync(string endpoint);
   }
}
