using System.Text.Json;

namespace ExchangeRates.Infrastructure.Extension
{
    public static class JsonExtention
    {
        public static T ToClassOf<T>(this string json)
        {
            if (!string.IsNullOrEmpty(json))
                return JsonSerializer.Deserialize<T>(json);

            return default;
        }
    }
}
