namespace ExchangeRates.Application.Interfaces
{
   public interface IConfigureEndpoint
    {
        string Configure(string baseUrl, string accessKey);
    }
}
