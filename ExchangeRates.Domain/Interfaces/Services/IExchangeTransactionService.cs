using ExchangeRates.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExchangeRates.Domain.Interfaces.Services
{
    public interface IExchangeTransactionService
    {
        public Task Convert(Transaction transaction);

        public Task<IEnumerable<Transaction>> ListByUserId(int userId);
    }
}
