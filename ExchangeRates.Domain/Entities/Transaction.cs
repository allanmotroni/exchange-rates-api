using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExchangeRates.Domain.Entities
{
    public class Transaction
    {
        public int TransactionId { get; set; }
        public int UserId { get; set; }
        public User User { get; set; }
        public string FromCurrency { get; set; }
        public double FromValue { get; set; }
        public double ToCurrency { get; set; }
        public double ToValue { get; set; }
        public double Rate { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}
