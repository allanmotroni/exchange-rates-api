using ExchangeRates.Application.Services;
using ExchangeRates.Domain.Entities;
using ExchangeRates.Domain.Entities.Validations;
using ExchangeRates.Domain.Validations;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Linq;

namespace ExchangeRates.Test.Domain.Validations
{
   [TestClass]
    public class CreateTransactionValidationTest
    {
        private readonly ICustomValidator _customValidator;
        private readonly ValidationService _validationService;

        public CreateTransactionValidationTest()
        {
            _customValidator = new CustomValidator();
            _validationService = new ValidationService(_customValidator);
        }

        private Transaction GetBaseTransaction()
        {
            return new Transaction()
            {
                UserId = 1,
                CreatedAt = DateTime.UtcNow,
                FromCurrency = "AAA",
                FromValue = 100,
                Rate = 1.5,
                ToCurrency = "BBB",
                TransactionId = 1
            };
        }

        [TestMethod]
        public void Given_Transaction_With_FromCurrency_Length_Different_Than_Three()
        {
            var transaction = GetBaseTransaction();
            transaction.FromCurrency = "ABCD";

            _validationService.Validate(transaction, new CreateTransactionValidation());

            Assert.AreEqual(true, _customValidator.GetValidations().Count() == 1);
        }

        [TestMethod]
        public void Given_Transaction_With_FromCurrency_Length_Iquals_Three()
        {
            var transaction = GetBaseTransaction();
            transaction.FromCurrency = "ABC";

            _validationService.Validate(transaction, new CreateTransactionValidation());

            Assert.AreEqual(false, _customValidator.HasErrors());
        }

        [TestMethod]
        public void Given_Transaction_With_Null_FromCurrency()
        {
            var transaction = GetBaseTransaction();
            transaction.FromCurrency = null;

            _validationService.Validate(transaction, new CreateTransactionValidation());

            Assert.AreEqual(true, _customValidator.GetValidations().Count() == 2);
        }

        [TestMethod]
        public void Given_Transaction_With_Empty_FromCurrency()
        {
            var transaction = GetBaseTransaction();
            transaction.FromCurrency = string.Empty;

            _validationService.Validate(transaction, new CreateTransactionValidation());

            Assert.AreEqual(true, _customValidator.GetValidations().Count() == 2);
        }

        

        [TestMethod]
        public void Given_Transaction_With_ToCurrency_Length_Different_Than_Three()
        {
            var transaction = GetBaseTransaction();
            transaction.ToCurrency = "ABCD";

            _validationService.Validate(transaction, new CreateTransactionValidation());

            Assert.AreEqual(true, _customValidator.GetValidations().Count() == 1);
        }

        [TestMethod]
        public void Given_Transaction_With_ToCurrency_Length_Iquals_Three()
        {
            var transaction = GetBaseTransaction();
            transaction.ToCurrency = "ABC";

            _validationService.Validate(transaction, new CreateTransactionValidation());

            Assert.AreEqual(false, _customValidator.HasErrors());
        }

        [TestMethod]
        public void Given_Transaction_With_Null_ToCurrency()
        {
            var transaction = GetBaseTransaction();
            transaction.ToCurrency = null;

            _validationService.Validate(transaction, new CreateTransactionValidation());

            Assert.AreEqual(true, _customValidator.GetValidations().Count() == 2);
        }

        [TestMethod]
        public void Given_Transaction_With_Empty_ToCurrency()
        {
            var transaction = GetBaseTransaction();
            transaction.ToCurrency = string.Empty;

            _validationService.Validate(transaction, new CreateTransactionValidation());

            Assert.AreEqual(true, _customValidator.GetValidations().Count() == 2);
        }

        [TestMethod]
        public void Given_Transaction_With_FromValue_Zero()
        {
            var transaction = GetBaseTransaction();
            transaction.FromValue = 0;

            _validationService.Validate(transaction, new CreateTransactionValidation());

            Assert.AreEqual(true, _customValidator.GetValidations().Count() == 1);
        }



    }
}
