using ExchangeRates.Domain.Interfaces.Logger;
using ExchangeRates.Domain.Validations;
using ExchangeRates.Infrastructure.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace ExchangeRates.Test.Infrastructure.Services
{
    [TestClass]
    public class ConfiguraExchangeRatesEndpointServiceTest
    {
        [TestMethod]
        public void Given_A_Valid_FromCurrency_And_A_Valid_ToCurrency_But_Without_API_Configuration_Parameters_Should_Return_Null_Json()
        {
            //Arrange
            var mockConfiguration = new Mock<IConfiguration>();
            var configuration = mockConfiguration.Object;

            var mockCustomLogger = new Mock<ICustomLogger>();
            var customLogger = mockCustomLogger.Object;

            var mockCustomValidator = new Mock<ICustomValidator>();
            var customValidator = mockCustomValidator.Object;

            string fromCurrrency = "EUR";
            string toCurrrency = "USD";

            ConfigureExchangeRatesEndpointService configuraExchangeRatesEndpointService = new ConfigureExchangeRatesEndpointService(configuration, customLogger, customValidator);
            
            //Act
            string json = configuraExchangeRatesEndpointService.Configure(fromCurrrency, toCurrrency);

            //Assert
            Assert.IsNull(json);
        }
    }
}
