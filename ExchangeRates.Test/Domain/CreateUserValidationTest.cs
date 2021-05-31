using ExchangeRates.Domain.Entities;
using ExchangeRates.Domain.Entities.Validations;
using ExchangeRates.Domain.Services;
using ExchangeRates.Domain.Validations;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExchangeRates.Test.Domain
{
    [TestClass]
    public class CreateUserValidationTest
    {
        private readonly ICustomValidator _customValidator;
        private readonly ValidationService _validationService;

        public CreateUserValidationTest()
        {
            _customValidator = new Validator();
            _validationService = new ValidationService(_customValidator);
        }

        private User GetBaseUser()
        {
            return new User()
            {
                UserId = 1,
                Name = "User Name Test",
                Email = "teste@teste.com",
                CreatedAt = DateTime.UtcNow,
                Active = true
            };
        }

        [TestMethod]
        public void Given_Transaction_With_Name()
        {
            var user = GetBaseUser();

            _validationService.Validate(user, new CreateUserValidation());

            Assert.AreEqual(false, _customValidator.HasErrors());
        }

        [TestMethod]
        public void Given_Transaction_With_Empty_Name()
        {
            var user = GetBaseUser();
            user.Name = string.Empty;

            _validationService.Validate(user, new CreateUserValidation());

            Assert.AreEqual(true, _customValidator.GetValidations().Count() == 2);
        }

        [TestMethod]
        public void Given_Transaction_Name_With_Whitespaces()
        {
            var user = GetBaseUser();
            user.Name = " ";

            _validationService.Validate(user, new CreateUserValidation());

            Assert.AreEqual(true, _customValidator.GetValidations().Count() == 2);
        }

        [TestMethod]
        public void Given_Transaction_With_ValidEmail()
        {
            var user = GetBaseUser();

            _validationService.Validate(user, new CreateUserValidation());

            Assert.AreEqual(false, _customValidator.HasErrors());
        }

        [TestMethod]
        public void Given_Transaction_With_InvalidEmail()
        {
            var user = GetBaseUser();
            user.Email = "invalid_email@";

            _validationService.Validate(user, new CreateUserValidation());

            Assert.AreEqual(true, _customValidator.GetValidations().Count() == 1);
        }


    }
}
