using ExchangeRates.Application.Services;
using ExchangeRates.Domain.Entities.Validations;
using ExchangeRates.Domain.Validations;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ExchangeRates.Test.Domain.Validations
{
   [TestClass]
    public class CustomEmailValidationTest
    {

        private readonly ICustomValidator _customValidator;
        private readonly ValidationService _validationService;

        public CustomEmailValidationTest()
        {
            _customValidator = new CustomValidator();
            _validationService = new ValidationService(_customValidator);
        }

        [TestMethod]
        public void Given_ValidEmail()
        {
            var email = "test@test.com";

            _validationService.Validate(email, new CustomEmailValidation());

            Assert.AreEqual(false, _customValidator.HasErrors());
        }

        [TestMethod]
        public void Given_InvalidEmail()
        {
            var email = "test@";

            _validationService.Validate(email, new CustomEmailValidation());

            Assert.AreEqual(true, _customValidator.HasErrors());
        }

    }
}