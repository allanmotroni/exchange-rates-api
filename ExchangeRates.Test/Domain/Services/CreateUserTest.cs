using ExchangeRates.Domain.Entities;
using ExchangeRates.Domain.Interfaces.Repositories;
using ExchangeRates.Domain.Services;
using ExchangeRates.Domain.Validations;
using ExchangeRates.Test.Infrastructure.Repositories;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Threading.Tasks;

namespace ExchangeRates.Test.Domain.Services
{
    [TestClass]
    public class CreateUserTest
    {
        private readonly IUserRepository _userRepositoryFake;
        private readonly ValidationService _validationService;
        private readonly ICustomValidator _customValidator;

        public CreateUserTest()
        {
            _customValidator = new Validator();
            _validationService = new ValidationService(_customValidator);
            _userRepositoryFake = new UserRepositoryFake();
        }

        [TestMethod]
        public async Task Given_A_Valid_User_Should_Create()
        {
            //Arrange
            UserService userService = new UserService(_userRepositoryFake, _validationService, _customValidator);
            User user = new User { Name = "Test 1", Email = "test@test.com", Active = true, CreatedAt = DateTime.Now };

            //Act
            await userService.Create(user);

            //Assert
            Assert.IsTrue(user.UserId > 0);
        }

        [TestMethod]
        public async Task Given_A_Valid_User_With_Email_That_Already_Exists_Should_Not_Create()
        {
            //Arrange
            string email = "test@test.com";
           
            User user = new User { Name = "Test1", Email = email, Active = true, CreatedAt = DateTime.Now };

            var mockUserRepository = new Mock<IUserRepository>();

            mockUserRepository.Setup(r => r.GetByEmail(It.IsAny<string>()))
                .ReturnsAsync(user);
                
            var userRepository = mockUserRepository.Object;

            var userService = new UserService(userRepository,_validationService,_customValidator);

            //Act
            await userService.Create(user);

            //Assert
            Assert.IsTrue(_customValidator.HasErrors());
        }

        [TestMethod]
        public async Task Given_A_User_With_Invalid_Email_Should_Not_Create()
        {
            //Arrange
            string email = "invalid_email@";

            User user = new User { Name = "Test1", Email = email, Active = true, CreatedAt = DateTime.Now };

            var mockUserRepository = new Mock<IUserRepository>();

            var userRepository = mockUserRepository.Object;

            var userService = new UserService(userRepository, _validationService, _customValidator);

            //Act
            await userService.Create(user);

            //Assert
            Assert.IsTrue(_customValidator.HasErrors());
        }

        [TestMethod]
        public async Task Given_A_User_Without_Name_Should_Not_Create()
        {
            //Arrange
            User user = new User { Name = "", Email = "test@test.com", Active = true, CreatedAt = DateTime.Now };

            var mockUserRepository = new Mock<IUserRepository>();

            var userRepository = mockUserRepository.Object;

            var userService = new UserService(userRepository, _validationService, _customValidator);

            //Act
            await userService.Create(user);

            //Assert
            Assert.IsTrue(_customValidator.HasErrors());
        }
    }
}
