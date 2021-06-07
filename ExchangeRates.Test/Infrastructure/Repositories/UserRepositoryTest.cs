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
    public class UserRepositoryTest
    {
        private User _user;
        private readonly DatabaseContext _databaseContext;

        public UserRepositoryTest()
        {
            var options = new DbContextOptionsBuilder<DatabaseContext>()
                .UseInMemoryDatabase("DbInMemoryContext")
                .Options;
            _databaseContext = new DatabaseContext(options);

            _user = new User { Name = "Test1", Active = true, CreatedAt = DateTime.Now, Email = "test@email.com" };
        }

        [TestMethod]
        public async Task Given_An_User_Should_Create_It()
        {
            //Arrange
            var mockCustomLogger = new Mock<ICustomLogger>();
            var customLogger = mockCustomLogger.Object;

            UserRepository userRepository = new UserRepository(_databaseContext, customLogger);

            //Act
            await userRepository.Create(_user);

            //Assert
            Assert.IsTrue(_user.UserId > 0);
        }

        [TestMethod]
        public async Task Given_An_UserId_Should_Return_An_User()
        {
            //Arrange
            var mockCustomLogger = new Mock<ICustomLogger>();
            var customLogger = mockCustomLogger.Object;

            UserRepository userRepository = new UserRepository(_databaseContext, customLogger);
            await userRepository.Create(_user);

            //Act
            User userReturned = await userRepository.GetById(_user.UserId);

            //Assert
            Assert.IsNotNull(userReturned);
        }

        [TestMethod]
        public async Task Given_An_Email_Should_Return_An_User()
        {
            //Arrange
            var mockCustomLogger = new Mock<ICustomLogger>();
            var customLogger = mockCustomLogger.Object;

            UserRepository userRepository = new UserRepository(_databaseContext, customLogger);
            await userRepository.Create(_user);

            //Act
            User userReturned = await userRepository.GetByEmail(_user.Email);

            //Assert
            Assert.IsNotNull(userReturned);
        }

        [TestMethod]
        public async Task Should_Return_At_Least_Three_Users()
        {
            //Arrange
            var mockCustomLogger = new Mock<ICustomLogger>();
            var customLogger = mockCustomLogger.Object;

            UserRepository userRepository = new UserRepository(_databaseContext, customLogger);
            await userRepository.Create(new User { Name = "Test1", Active = true, CreatedAt = DateTime.Now, Email = "test1@email.com" });
            await userRepository.Create(new User { Name = "Test2", Active = true, CreatedAt = DateTime.Now, Email = "test2@email.com" });
            await userRepository.Create(new User { Name = "Test3", Active = true, CreatedAt = DateTime.Now, Email = "test3@email.com" });

            //Act
            IList<User> usersReturned = await userRepository.GetAll();

            //Assert
            Assert.IsTrue(usersReturned.Count >= 3);
        }

    }
}
