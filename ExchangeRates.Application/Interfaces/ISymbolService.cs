
using ExchangeRates.Domain.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ExchangeRates.Application.Interfaces
{
   public interface ISymbolService
   {
      Task<IList<Symbol>> GetAll();
   }
}