using ExchangeRates.Domain.Entities;
using ExchangeRates.Domain.Interfaces.Repositories;
using ExchangeRates.Domain.Services;
using ExchangeRates.Domain.Validations;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Threading.Tasks;

namespace ExchangeRates.Test.Domain.Services
{
    [TestClass]
    public class FindByEmailTest
    {
        private readonly ValidationService _validationService;
        private readonly ICustomValidator _customValidator;

        public FindByEmailTest()
        {
            _customValidator = new Validator();
            _validationService = new ValidationService(_customValidator);
        }

        [TestMethod]
        public async Task Given_A_Valid_Email_That_Is_In_Database_Should_Find_An_User()
        {
            //Arrange
            string email = "test@test.com";
            User user = new User { Name = "Test1", Email = email, Active = true, CreatedAt = DateTime.Now };
            
            var mockUserRepository = new Mock<IUserRepository>();

            mockUserRepository.Setup(r => r.GetByEmail(It.IsAny<string>()))
                .ReturnsAsync(user);

            var userRepository = mockUserRepository.Object;

            var userService = new UserService(userRepository, _validationService, _customValidator);

            //Act
            var userReturned = await userService.FindByEmail(email);

            //Assert
            Assert.IsNotNull(userReturned);
        }

        [TestMethod]
        public async Task Given_An_Invalid_Email_Should_Not_Return_User()
        {
            //Arrange
            string email = "invalid_email";
            User user = new User { Name = "Test1", Email = email, Active = true, CreatedAt = DateTime.Now };

            var mockUserRepository = new Mock<IUserRepository>();

            mockUserRepository.Setup(r => r.GetByEmail(It.IsAny<string>()))
                .ReturnsAsync(user);

            var userRepository = mockUserRepository.Object;

            var userService = new UserService(userRepository, _validationService, _customValidator);

            //Act
            var userReturned = await userService.FindByEmail(email);

            //Assert
            Assert.IsNull(userReturned);
        }

        [TestMethod]
        public async Task Given_An_Invalid_Email_Should_Not_Be_Invalid()
        {
            //Arrange
            string email = "invalid_email";
            User user = new User { Name = "Test1", Email = email, Active = true, CreatedAt = DateTime.Now };

            var mockUserRepository = new Mock<IUserRepository>();

            mockUserRepository.Setup(r => r.GetByEmail(It.IsAny<string>()))
                .ReturnsAsync(user);

            var userRepository = mockUserRepository.Object;

            var userService = new UserService(userRepository, _validationService, _customValidator);

            //Act
            var userReturned = await userService.FindByEmail(email);

            //Assert
            Assert.IsTrue(_customValidator.HasErrors());
        }
    }
}
