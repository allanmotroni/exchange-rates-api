using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExchangeRates.Domain.Dto
{
    public class NewUserDto
    {
        public string Name { get; set; }
        public string Email { get; set; }

        public override string ToString()
        {
            return $"Name: {Name} - Email: {Email}";
        }
    }
}
