using ExchangeRates.Domain.Entities;
using ExchangeRates.Domain.Interfaces.Repositories;
using ExchangeRates.Domain.Services;
using ExchangeRates.Domain.Validations;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;


namespace ExchangeRates.Test.Domain.Services
{
    [TestClass]
    public class ExchangeTransactionServiceTest
    {
        private readonly ValidationService _validationService;
        private readonly ICustomValidator _customValidator;

        public ExchangeTransactionServiceTest()
        {
            _customValidator = new CustomValidator();
            _validationService = new ValidationService(_customValidator);

        }

        [TestMethod]
        public async Task Given_An_Invalid_Transaction_Should_Return_A_Error_Validation()
        {
            //Arrange
            Transaction transaction = new Transaction { FromCurrency = "EUR", FromValue = 1, ToCurrency = "ERROR", UserId = 1 };

            var mockUserRepository = new Mock<IUserRepository>();
            var userRepository = mockUserRepository.Object;

            var mockTransactionRepository = new Mock<ITransactionRepository>();
            var transactionRepository = mockTransactionRepository.Object;

            var mockExchangeRateRepository = new Mock<IExchangeRateRepository>();
            var exchangeRateRepository = mockExchangeRateRepository.Object;

            ExchangeTransactionService exchangeTransactionService = new ExchangeTransactionService(_validationService, _customValidator, transactionRepository, userRepository, exchangeRateRepository);

            //Act
            await exchangeTransactionService.Convert(transaction);

            //Assert
            Assert.IsTrue(_customValidator.HasErrors());
        }

        [TestMethod]
        public async Task Given_A_Valid_Transaction_Should_Not_Return_A_Error_Validation()
        {
            //Arrange
            Transaction transaction = new Transaction { FromCurrency = "EUR", FromValue = 1, ToCurrency = "BRL", UserId = 1 };

            User user = new User { UserId = 1, Active = true, CreatedAt = DateTime.Now, Name = "Test", Email = "test@test.com" };

            var mockUserRepository = new Mock<IUserRepository>();
            mockUserRepository.Setup(user => user.GetById(It.IsAny<int>()))
                .ReturnsAsync(user);
            var userRepository = mockUserRepository.Object;

            var mockTransactionRepository = new Mock<ITransactionRepository>();
            var transactionRepository = mockTransactionRepository.Object;

            var mockExchangeRateRepository = new Mock<IExchangeRateRepository>();
            var exchangeRateRepository = mockExchangeRateRepository.Object;

            ExchangeTransactionService exchangeTransactionService = new ExchangeTransactionService(_validationService, _customValidator, transactionRepository, userRepository, exchangeRateRepository);

            //Act
            await exchangeTransactionService.Convert(transaction);

            //Assert
            Assert.IsFalse(_customValidator.HasErrors());
        }

        [TestMethod]
        public async Task Given_An_Invalid_User_Should_Return_A_Error_Validation()
        {
            //Arrange
            Transaction transaction = new Transaction { FromCurrency = "EUR", FromValue = 1, ToCurrency = "BRL", UserId = 0 };

            var mockUserRepository = new Mock<IUserRepository>();
            mockUserRepository.Setup(user => user.GetById(It.IsAny<int>()))
                .ReturnsAsync((User)null);
            var userRepository = mockUserRepository.Object;

            var mockTransactionRepository = new Mock<ITransactionRepository>();
            var transactionRepository = mockTransactionRepository.Object;

            var mockExchangeRateRepository = new Mock<IExchangeRateRepository>();
            var exchangeRateRepository = mockExchangeRateRepository.Object;

            ExchangeTransactionService exchangeTransactionService = new ExchangeTransactionService(_validationService, _customValidator, transactionRepository, userRepository, exchangeRateRepository);

            //Act
            await exchangeTransactionService.Convert(transaction);

            //Assert
            Assert.IsTrue(_customValidator.HasErrors());
        }

        [TestMethod]
        public async Task Given_A_Valid_User_Should_Not_Return_A_Error_Validation()
        {
            //Arrange
            Transaction transaction = new Transaction { FromCurrency = "EUR", FromValue = 1, ToCurrency = "BRL", UserId = 10 };
            User user = new User { UserId = 1, Active = true, CreatedAt = DateTime.Now, Name = "Test", Email = "test@test.com" };

            var mockUserRepository = new Mock<IUserRepository>();
            mockUserRepository.Setup(user => user.GetById(It.IsAny<int>()))
                .ReturnsAsync(user);
            var userRepository = mockUserRepository.Object;

            var mockTransactionRepository = new Mock<ITransactionRepository>();
            var transactionRepository = mockTransactionRepository.Object;

            var mockExchangeRateRepository = new Mock<IExchangeRateRepository>();
            var exchangeRateRepository = mockExchangeRateRepository.Object;

            ExchangeTransactionService exchangeTransactionService = new ExchangeTransactionService(_validationService, _customValidator, transactionRepository, userRepository, exchangeRateRepository);

            //Act
            await exchangeTransactionService.Convert(transaction);

            //Assert
            Assert.IsFalse(_customValidator.HasErrors());
        }

        [TestMethod]
        public async Task Given_A_Valid_User_And_Valid_Transaction_Should_Return_Rate()
        {
            //Arrange
            Transaction transaction = new Transaction { FromCurrency = "EUR", FromValue = 1, ToCurrency = "BRL", UserId = 1 };

            User user = new User { UserId = 1, Active = true, CreatedAt = DateTime.Now, Name = "Test", Email = "test@test.com" };
            double rate = 6.0;

            var mockUserRepository = new Mock<IUserRepository>();
            mockUserRepository.Setup(user => user.GetById(It.IsAny<int>()))
                .ReturnsAsync(user);
            var userRepository = mockUserRepository.Object;

            var mockTransactionRepository = new Mock<ITransactionRepository>();
            var transactionRepository = mockTransactionRepository.Object;

            var mockExchangeRateRepository = new Mock<IExchangeRateRepository>();
            mockExchangeRateRepository.Setup(exchange => exchange.GetExchangeRate(It.IsAny<string>(), It.IsAny<string>()))
                .ReturnsAsync(rate);
            var exchangeRateRepository = mockExchangeRateRepository.Object;

            ExchangeTransactionService exchangeTransactionService = new ExchangeTransactionService(_validationService, _customValidator, transactionRepository, userRepository, exchangeRateRepository);

            //Act
            await exchangeTransactionService.Convert(transaction);

            //Assert
            Assert.AreEqual(transaction.Rate, rate);
        }

        [TestMethod]
        public async Task Given_A_Valid_UserId_With_Transactions_Should_Return_Two_Transactions()
        {
            //Arrange
            IList<Transaction> transactions = new List<Transaction> {
                new Transaction { FromCurrency = "EUR", FromValue = 1, ToCurrency = "BRL", UserId = 1 },
                new Transaction { FromCurrency = "EUR", FromValue = 1, ToCurrency = "USD", UserId = 1 }
            };

            User user = new User { UserId = 1, Active = true, CreatedAt = DateTime.Now, Name = "Test", Email = "test@test.com" };
            
            var mockUserRepository = new Mock<IUserRepository>();
            mockUserRepository.Setup(user => user.GetById(It.IsAny<int>()))
                .ReturnsAsync(user);
            var userRepository = mockUserRepository.Object;

            var mockTransactionRepository = new Mock<ITransactionRepository>();
            mockTransactionRepository.Setup(t => t.ListByUserId(It.IsAny<int>()))
                .ReturnsAsync(transactions);
            var transactionRepository = mockTransactionRepository.Object;

            var mockExchangeRateRepository = new Mock<IExchangeRateRepository>();            
            var exchangeRateRepository = mockExchangeRateRepository.Object;

            ExchangeTransactionService exchangeTransactionService = new ExchangeTransactionService(_validationService, _customValidator, transactionRepository, userRepository, exchangeRateRepository);

            //Act
            IList<Transaction> transactionsReturned = await exchangeTransactionService.ListByUserId(user.UserId);

            //Assert
            Assert.AreEqual(transactionsReturned.Count, transactions.Count);
        }

        [TestMethod]
        public async Task Given_A_Valid_UserId_Without_Transactions_Should_Not_Return_Transactions()
        {
            //Arrange
            User user = new User { UserId = 1, Active = true, CreatedAt = DateTime.Now, Name = "Test", Email = "test@test.com" };

            var mockUserRepository = new Mock<IUserRepository>();
            mockUserRepository.Setup(user => user.GetById(It.IsAny<int>()))
                .ReturnsAsync(user);
            var userRepository = mockUserRepository.Object;

            var mockTransactionRepository = new Mock<ITransactionRepository>();
            mockTransactionRepository.Setup(t => t.ListByUserId(It.IsAny<int>()))
                .ReturnsAsync(new List<Transaction>());
            var transactionRepository = mockTransactionRepository.Object;

            var mockExchangeRateRepository = new Mock<IExchangeRateRepository>();
            var exchangeRateRepository = mockExchangeRateRepository.Object;

            ExchangeTransactionService exchangeTransactionService = new ExchangeTransactionService(_validationService, _customValidator, transactionRepository, userRepository, exchangeRateRepository);

            //Act
            IList<Transaction> transactionsReturned = await exchangeTransactionService.ListByUserId(user.UserId);

            //Assert
            Assert.AreEqual(0, transactionsReturned.Count);
        }

        [TestMethod]
        public async Task Given_A_Invalid_UserId_Should_Return_A_Validation()
        {
            //Arrange
            User user = null;

            var mockUserRepository = new Mock<IUserRepository>();
            mockUserRepository.Setup(user => user.GetById(It.IsAny<int>()))
                .ReturnsAsync(user);
            var userRepository = mockUserRepository.Object;

            var mockTransactionRepository = new Mock<ITransactionRepository>();
            var transactionRepository = mockTransactionRepository.Object;

            var mockExchangeRateRepository = new Mock<IExchangeRateRepository>();
            var exchangeRateRepository = mockExchangeRateRepository.Object;

            ExchangeTransactionService exchangeTransactionService = new ExchangeTransactionService(_validationService, _customValidator, transactionRepository, userRepository, exchangeRateRepository);

            //Act
            IList<Transaction> transactionsReturned = await exchangeTransactionService.ListByUserId(0);

            //Assert
            Assert.IsTrue(_customValidator.HasErrors());
        }

    }
}
