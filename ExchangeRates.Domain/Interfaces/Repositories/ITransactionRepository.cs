using ExchangeRates.Domain.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ExchangeRates.Domain.Interfaces.Repositories
{
    public interface ITransactionRepository
    {
        Task Create(Transaction transaction);
        
        Task<IList<Transaction>> ListByUserId(int userId);
    }
}
