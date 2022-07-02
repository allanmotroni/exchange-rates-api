using ExchangeRates.Application.Interfaces;
using ExchangeRates.Application.Services;
using ExchangeRates.Domain.Interfaces.Logger;
using ExchangeRates.Domain.Validations;
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

            var mockGetService = new Mock<IGetService>();
            mockGetService.Setup(post => post.GetAsync(It.IsAny<string>()))
                .ReturnsAsync(jsonReturned);
            var getService = mockGetService.Object;

            var mockConfigureEndpoint = new Mock<IConfigureEndpoint>();
            var configureEndpoint = mockConfigureEndpoint.Object;

            IExchangeRateService exchangeRateService = new ExchangeRateService(getService, configureEndpoint);

            //Act
            var rate = await exchangeRateService.GetExchangeRate(fromCurrency, toCurrency);

            //Assert
            Assert.AreEqual(rate, 1.21633);
        }

    }
}
