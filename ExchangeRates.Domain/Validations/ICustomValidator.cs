using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
