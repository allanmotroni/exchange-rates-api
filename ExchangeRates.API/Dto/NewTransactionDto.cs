﻿namespace ExchangeRates.Domain.API
{
    public class NewTransactionDto
    {
        public int UserId { get; set; }
        public string FromCurrency { get; set; }
        public double FromValue { get; set; }

        public override string ToString()
        {
            return $"Id: {UserId} - FromCurrency: {FromCurrency} - FromValue: {FromValue}";
        }
    }
}
