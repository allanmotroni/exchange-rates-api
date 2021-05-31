using System;

namespace ExchangeRates.API
{
    public class TransactionDto
    {
        public int TransactionId { get; set; }
        public int UserId { get; set; }
        public string FromCurrency { get; set; }
        public double FromValue { get; set; }
        public string ToCurrency { get; set; }
        public double ToValue { get; set; }

        public double Rate { get; set; }
        public DateTime CreatedAt { get; set; }

        public override string ToString()
        {
            return $"Id: {TransactionId} - UserId: {UserId} - FromCurrency: {FromCurrency} - FromValue: {FromValue} - ToCurrency: {ToCurrency} - ToValue: {ToValue} - Rate: {Rate} - CreatedAt: {CreatedAt}";
        }
    }
}
