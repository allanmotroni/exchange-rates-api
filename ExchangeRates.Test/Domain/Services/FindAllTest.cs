using ExchangeRates.Application.Services;
using ExchangeRates.Domain.Entities;
using ExchangeRates.Domain.Interfaces.Repositories;
using ExchangeRates.Domain.Validations;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ExchangeRates.Test.Domain.Services
{
   [TestClass]
    public class FindAllTest
    {
        private ValidationService _validationService;
        private readonly ICustomValidator _customValidator;

        public FindAllTest()
        {
            _customValidator = new CustomValidator();
            _validationService = new ValidationService(_customValidator);
        }

        [TestMethod]
        public async Task Should_Return_Three_Users()
        {
            //Arrange
            var users = new List<User> {
                new User{ UserId = 1, Active = true, CreatedAt = DateTime.Now, Email = "test1@email.com", Name = "Test1" },
                new User{ UserId = 2, Active = true, CreatedAt = DateTime.Now, Email = "test2@email.com", Name = "Test2" },
                new User{ UserId = 3, Active = true, CreatedAt = DateTime.Now, Email = "test3@email.com", Name = "Test3" }
            };

            var mockUserRepository = new Mock<IUserRepository>();
            mockUserRepository.Setup(user => user.GetAll())
                .ReturnsAsync(users);
            var userRepository = mockUserRepository.Object;

            UserService userService = new UserService(userRepository, _validationService, _customValidator);

            //Act
            IList<User> usersReturned = await userService.FindAll();

            //Assert
            Assert.IsTrue(usersReturned.Count > 0);
        }

        [TestMethod]
        public async Task Should_Return_No_Users()
        {
            //Arrange
            var mockUserRepository = new Mock<IUserRepository>();
            mockUserRepository.Setup(user => user.GetAll())
                .ReturnsAsync(new List<User>());
            var userRepository = mockUserRepository.Object;

            UserService userService = new UserService(userRepository, _validationService, _customValidator);

            //Act
            IList<User> users = await userService.FindAll();

            //Assert
            Assert.IsTrue(users.Count == 0);
        }
    }
}
