using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace ExchangeRates.Infrastructure.Dto
{
    [Serializable]
    public class ExchangeRatesDto
    {
        [JsonPropertyName("success")]
        public bool Success { get; set; }

        [JsonPropertyName("timestamp")]
        public int TimeStamp { get; set; }

        [JsonPropertyName("base")]
        public string Base { get; set; }

        [JsonPropertyName("date")]
        public DateTime Date { get; set; }

        [JsonPropertyName("rates")]
        public IDictionary<string, double> Rates { get; set; }
    }
}
