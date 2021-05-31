using ExchangeRates.Domain.Entities.Validations;
using ExchangeRates.Domain.Services;
using ExchangeRates.Domain.Validations;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ExchangeRates.Test.Domain
{
    [TestClass]
    public class CustomStringValidationTest
    {

        private readonly ICustomValidator _customValidator;
        private readonly ValidationService _validationService;

        public CustomStringValidationTest()
        {
            _customValidator = new Validator();
            _validationService = new ValidationService(_customValidator);
        }

        [TestMethod]
        public void Given_ValidEmail()
        {
            var email = "test@test.com";

            _validationService.Validate(email, new CustomStringValidation());

            Assert.AreEqual(false, _customValidator.HasErrors());
        }

        [TestMethod]
        public void Given_InvalidEmail()
        {
            var email = "test@";

            _validationService.Validate(email, new CustomStringValidation());

            Assert.AreEqual(true, _customValidator.HasErrors());
        }

    }
}