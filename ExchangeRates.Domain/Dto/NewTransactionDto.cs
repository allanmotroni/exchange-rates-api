using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExchangeRates.Domain.Dto
{
    public class NewTransactionDto
    {
        public int UserId { get; set; }
        public string FromCurrency { get; set; }
        public double FromValue { get; set; }        
    }
}
