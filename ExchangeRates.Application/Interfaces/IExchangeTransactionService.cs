using ExchangeRates.Domain.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ExchangeRates.Application.Interfaces
{
    public interface IExchangeTransactionService
    {
        Task Convert(Transaction transaction);

        Task<IList<Transaction>> ListByUserId(int userId);
    }
}
