using System;

namespace ExchangeRates.Domain.API
{
    public class TransactionDto
    {
        public int UserId { get; set; }
        public string FromCurrency { get; set; }
        public double FromValue { get; set; }
        public double ToCurrency { get; set; }
        public double Rate { get; set; }
        public DateTime CreatedAt { get; set; }

        public override string ToString()
        {
            return $"UserId: {UserId} - FromCurrency: {FromCurrency} - FromValue: {FromValue} - ToCurrency: {ToCurrency} - Rate: {Rate} - CreatedAt: {CreatedAt}";
        }
    }
}
