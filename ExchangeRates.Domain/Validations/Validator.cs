using System.Collections.Generic;
using System.Linq;

namespace ExchangeRates.Domain.Validations
{
    public class Validator : ICustomValidator
    {
        private readonly IList<string> _validations;
        public Validator()
        {
            _validations = new List<string>();
        }

        public void Notify(string message)
        {
            Handle(message);
        }
        
        public bool HasErrors() => _validations.Any();

        public IEnumerable<string> GetValidations()
        {
            return _validations;
        }

        public string GetStringValidations()
        {
            return string.Join("| ",_validations);
        }

        private void Handle(string message)
        {
            _validations.Add(message);
        }
    }
}
