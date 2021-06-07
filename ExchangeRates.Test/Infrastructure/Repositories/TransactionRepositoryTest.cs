using ExchangeRates.Domain.Entities;
using ExchangeRates.Domain.Interfaces.Logger;
using ExchangeRates.Infrastructure.Data;
using ExchangeRates.Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ExchangeRates.Test.Infrastructure.Repositories
{
    [TestClass]
    public class TransactionRepositoryTest
    {
        private readonly DatabaseContext _databaseContext;
        private User _user;

        public TransactionRepositoryTest()
        {
            var options = new DbContextOptionsBuilder<DatabaseContext>()
                .UseInMemoryDatabase("DbInMemoryContext")
                .Options;
            _databaseContext = new DatabaseContext(options);

            _user = new User { Name = "Test1", Active = true, CreatedAt = DateTime.Now, Email = "test@email.com" };
        }

        [TestMethod]
        public async Task Given_A_Transaction_Should_Create_It()
        {
            //Arrange
            var mockCustomLogger = new Mock<ICustomLogger>();
            var customLogger = mockCustomLogger.Object;

            UserRepository userRepository = new UserRepository(_databaseContext, customLogger);
            var user = await userRepository.GetById(_user.UserId);
            if (user == null)
            {
                await userRepository.Create(_user);
            }
            else
            {
                _user = user;
            }

            TransactionRepository transactionRepository = new TransactionRepository(_databaseContext, customLogger);

            var transaction = new Transaction { CreatedAt = DateTime.Now, FromCurrency = "EUR", FromValue = 1, ToCurrency = "USD", UserId = _user.UserId };

            //Act
            await transactionRepository.Create(transaction);

            //Assert
            Assert.IsTrue(transaction.TransactionId > 0);
        }

        [TestMethod]
        public async Task Should_Return_Three_Transactions_At_Least()
        {
            //Arrange
            var mockCustomLogger = new Mock<ICustomLogger>();
            var customLogger = mockCustomLogger.Object;

            UserRepository userRepository = new UserRepository(_databaseContext, customLogger);
            var user = await userRepository.GetById(_user.UserId);
            if (user == null)
            {
                await userRepository.Create(_user);
            }
            else
            {
                _user = user;
            }

            TransactionRepository transactionRepository = new TransactionRepository(_databaseContext, customLogger);
            await transactionRepository.Create(new Transaction { CreatedAt = DateTime.Now, FromCurrency = "EUR", FromValue = 1, ToCurrency = "USD", UserId = _user.UserId });
            await transactionRepository.Create(new Transaction { CreatedAt = DateTime.Now, FromCurrency = "EUR", FromValue = 1, ToCurrency = "BRL", UserId = _user.UserId });
            await transactionRepository.Create(new Transaction { CreatedAt = DateTime.Now, FromCurrency = "EUR", FromValue = 1, ToCurrency = "JPY", UserId = _user.UserId });

            //Act
            IList<Transaction> transactions = await transactionRepository.ListByUserId(_user.UserId);

            //Assert
            Assert.IsTrue(transactions.Count >= 3);
        }
    }
}
