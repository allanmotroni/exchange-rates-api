using FluentValidation;

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
