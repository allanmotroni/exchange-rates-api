using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExchangeRates.Domain.Entities.Validations
{
    public class CustomEmailValidation : AbstractValidator<string>
    {
        public CustomEmailValidation()
        {
            RuleFor(p => p).NotNull().WithMessage("Invalid E-mail.");
            RuleFor(p => p.Trim()).NotEmpty().WithMessage("Invalid E-mail.");            
            RuleFor(p => p).EmailAddress().WithMessage("Invalid E-mail.");
        }
    }
}
