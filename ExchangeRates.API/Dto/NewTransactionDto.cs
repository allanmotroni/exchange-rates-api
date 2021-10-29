using ExchangeRates.API.Interfaces;

namespace ExchangeRates.API
{
    public class NewTransactionDto : IUser
    {
        public int UserId { get; set; }
        public string FromCurrency { get; set; }
        public double FromValue { get; set; }

        public string ToCurrency { get; set; }

        public override string ToString()
        {
            return $"Id: {UserId} - FromCurrency: {FromCurrency} - FromValue: {FromValue} - ToCurrency: {ToCurrency}";
        }
    }
}
