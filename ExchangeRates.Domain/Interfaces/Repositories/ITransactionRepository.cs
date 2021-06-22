using ExchangeRates.Domain.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ExchangeRates.Domain.Interfaces.Repositories
{
    public interface ITransactionRepository
    {
        public Task Create(Transaction transaction);
        
        public Task<IList<Transaction>> ListByUserId(int userId);
    }
}
