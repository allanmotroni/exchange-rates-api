using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExchangeRates.Domain.Entities.Validations
{
    public class CreateUserValidation : AbstractValidator<User>
    {
        public CreateUserValidation()
        {
            RuleFor(p => p.Name).NotNull().WithMessage("Name cannot be null.");
            RuleFor(p => p.Name).NotEmpty().WithMessage("Name is empty.");
            RuleFor(p => p.Name.Trim()).NotEmpty().WithMessage("Name cannot has only whitespaces.");


            RuleFor(p => p.Email).NotNull().WithMessage("E-mail cannot be null.");
            RuleFor(p => p.Email).NotEmpty().WithMessage("E-mail cannot be empty.");
            RuleFor(p => p.Email.Trim()).NotEmpty().WithMessage("E-mail cannot has only whitespaces.");
            RuleFor(p => p.Email).EmailAddress().WithMessage("Invalid E-mail.");
        }
    }
}
