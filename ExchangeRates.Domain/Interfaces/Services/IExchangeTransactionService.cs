using ExchangeRates.Domain.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ExchangeRates.Domain.Interfaces.Services
{
    public interface IExchangeTransactionService
    {
        Task Convert(Transaction transaction);

        Task<IList<Transaction>> ListByUserId(int userId);
    }
}
