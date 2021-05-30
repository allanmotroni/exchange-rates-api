using System;

namespace ExchangeRates.Domain.Entities
{
    public class Transaction
    {
        public int TransactionId { get; set; }
        public int UserId { get; set; }
        public User User { get; set; }
        public string FromCurrency { get; set; }
        public double FromValue { get; set; }
        public string ToCurrency { get; set; }
        public double ToValue { get; set; }
        public double Rate { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}
