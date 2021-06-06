using ExchangeRates.Domain.Entities;
using ExchangeRates.Domain.Interfaces.Repositories;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ExchangeRates.Test.Domain.Services
{
    [TestClass]
    public class FindUserTest
    {
        [TestMethod]
        public async Task Find_All_Should_Not_Return_Any_User()
        {
            //Arrange
            var mockUserRepository = new Mock<IUserRepository>();

            mockUserRepository.Setup(repository => repository.GetAll())
                .ReturnsAsync(new List<User>());

            var userRepository = mockUserRepository.Object;

            //Act
            var users = await userRepository.GetAll();

            //Assert
            Assert.IsFalse(users.Any());
        }

        [TestMethod]
        public async Task Find_All_Should_Return_Three_Users()
        {
            //Arrange
            var mockUserRepository = new Mock<IUserRepository>();
            var users = new List<User>() {
            new User(){ UserId = 1, Active = true, CreatedAt = DateTime.Now, Email = "test1@test.com", Name = "Test1" },
            new User(){ UserId = 2, Active = true, CreatedAt = DateTime.Now, Email = "test2@test.com", Name = "Test2" },
            new User(){ UserId = 3, Active = true, CreatedAt = DateTime.Now, Email = "test3@test.com", Name = "Test3" }
            };

            mockUserRepository.Setup(repository => repository.GetAll())
                .ReturnsAsync(users);

            var userRepository = mockUserRepository.Object;

            //Act
            var usersReturned = await userRepository.GetAll();

            //Assert
            Assert.IsTrue(usersReturned.Count == 3);
        } 
    }
}
