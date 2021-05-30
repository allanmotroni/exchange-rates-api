using ExchangeRates.Domain.Validations;
using FluentValidation;

namespace ExchangeRates.Domain.Services
{
    public class ValidationService
    {
        private readonly ICustomValidator _customValidator;
        public ValidationService(ICustomValidator customValidator)
        {
            _customValidator = customValidator;
        }

        public void Validate<C, T>(C validateClass, T validatorClass)
            where C : class
            where T : AbstractValidator<C>
        {
            FluentValidation.Results.ValidationResult validationResult = validatorClass.Validate(validateClass);
            if (!validationResult.IsValid)
            {
                foreach (var error in validationResult.Errors)
                {
                    _customValidator.Notify(error.ErrorMessage);
                }
            }
        }
    }
}
