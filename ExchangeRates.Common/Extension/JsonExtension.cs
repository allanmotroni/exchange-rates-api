using System.Text.Json;

namespace ExchangeRates.Common.Extension
{
    public static class JsonExtension
    {
        public static T ToClassOf<T>(this string json)
        {
            if (!string.IsNullOrEmpty(json))
                return JsonSerializer.Deserialize<T>(json);

            return default;
        }
    }
}
