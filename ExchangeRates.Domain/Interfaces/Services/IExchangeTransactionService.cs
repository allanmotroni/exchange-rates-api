using ExchangeRates.Domain.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ExchangeRates.Domain.Interfaces.Services
{
    public interface IExchangeTransactionService
    {
        public Task Convert(Transaction transaction);

        public Task<IEnumerable<Transaction>> ListByUserId(int userId);
    }
}
