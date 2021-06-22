﻿using ExchangeRates.Domain.Interfaces.Logger;
using ExchangeRates.Domain.Validations;
using ExchangeRates.Infrastructure.Interfaces;
using ExchangeRates.Infrastructure.Repositories;
using Microsoft.Extensions.Configuration;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System.Threading.Tasks;

namespace ExchangeRates.Test.Infrastructure.Repositories
{
    [TestClass]
    public class ExchangeRateRepositoryTest
    {
        
        [TestMethod]
        public async Task Given_Valid_FromCurrency_And_Valid_ToCurrency_Should_Return_Rate()
        {
            //Arrange
            string fromCurrency = "EUR";
            string toCurrency = "USD";

            string jsonReturned = "{\"success\": true,\"timestamp\": 1623010457,\"base\": \"EUR\",\"date\": \"2021-06-06\",\"rates\": {\"USD\": 1.21633}}";

            var mockCustomValidator = new Mock<ICustomValidator>();
            var customValidator = mockCustomValidator.Object;

            var mockCustomLogger = new Mock<ICustomLogger>();
            var customLogger = mockCustomLogger.Object;

            var mockConfiguration = new Mock<IConfiguration>();
            var configuration = mockConfiguration.Object;

            var mockPostService = new Mock<IPostService>();
            mockPostService.Setup(post => post.Post(It.IsAny<string>()))
                .ReturnsAsync(jsonReturned);
            var postService = mockPostService.Object;

            var mockConfigureEndpoint = new Mock<IConfigureEndpoint>();
            var configureEndpoint = mockConfigureEndpoint.Object;

            ExchangeRateRepository exchangeRateRepository = new ExchangeRateRepository(customValidator, customLogger, configuration, postService, configureEndpoint);

            //Act
            var rate = await exchangeRateRepository.GetExchangeRate(fromCurrency, toCurrency);

            //Assert
            Assert.AreEqual(rate, 1.21633);
        }

    }
}
