using System.Collections.Generic;

namespace ExchangeRates.Domain.Validations
{
    public interface ICustomValidator
    {
        public void Notify(string message);
        
        public bool HasErrors();

        public IEnumerable<string> GetValidations();

        public string GetStringValidations();

    }
}
