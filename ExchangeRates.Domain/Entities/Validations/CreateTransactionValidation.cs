using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExchangeRates.Domain.Entities.Validations
{
    public class CreateTransactionValidation : AbstractValidator<Transaction>
    {
        public CreateTransactionValidation()
        {
            RuleFor(p => p.FromCurrency).NotNull().WithMessage("FromCurrency cannot be null.");
            RuleFor(p => p.FromCurrency).NotEmpty().WithMessage("FromCurrency cannot be empty.");
            RuleFor(p => p.FromCurrency).Length(3).WithMessage("FromCurrency must has 3 characters.");

            RuleFor(p => p.FromValue).GreaterThan(0).WithMessage("FromValue must be greater than 0.");

            RuleFor(p => p.ToCurrency).NotNull().WithMessage("ToCurrency cannot be null.");
            RuleFor(p => p.ToCurrency).NotEmpty().WithMessage("ToCurrency cannot be empty.");
            RuleFor(p => p.ToCurrency).Length(3).WithMessage("ToCurrency must has 3 characters.");
        }
    }
}
