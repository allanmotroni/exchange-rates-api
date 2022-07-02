using ExchangeRates.Application.Interfaces;

namespace ExchangeRates.Application.Dto
{
   public class SymbolDto : IUser
   {
      public int UserId { get;set; }

      public string Code { get; set; }
      public string Description { get; set; }
   }
}
