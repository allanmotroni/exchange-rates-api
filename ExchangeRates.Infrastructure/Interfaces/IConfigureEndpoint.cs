using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExchangeRates.Infrastructure.Interfaces
{
    public interface IConfigureEndpoint
    {
        public string Configure(params string[] parameters);
    }
}
