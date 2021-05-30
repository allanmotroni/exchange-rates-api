using ExchangeRates.Domain.Entities;
using ExchangeRates.Domain.Interfaces.Logger;
using ExchangeRates.Domain.Interfaces.Repositories;
using ExchangeRates.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ExchangeRates.Infrastructure.Repositories
{
    public class TransactionRepository : ITransactionRepository
    {
        private readonly DatabaseContext _databaseContext;
        private readonly ICustomLogger _logger;
        public TransactionRepository(DatabaseContext databaseContext, ICustomLogger logger)
        {
            _databaseContext = databaseContext;
            _logger = logger;
        }

        public async Task Create(Transaction transaction)
        {
            _logger.Info($"Creating a transaction on Database. {transaction?.ToString()}");

            await _databaseContext.Transaction.AddAsync(transaction);
            await _databaseContext.SaveChangesAsync();

            _logger.Info($"Transaction has been created on Database: {transaction?.ToString()}");
        }

        public async Task<IList<Transaction>> ListByUserId(int userId)
        {
            _logger.Info($"Getting all Transactions on Database by User. UserId: {userId}");

            IList<Transaction> transactions = await _databaseContext.Transaction.Where(t => t.UserId == userId).ToListAsync();

            return transactions;
        }
    }
}
