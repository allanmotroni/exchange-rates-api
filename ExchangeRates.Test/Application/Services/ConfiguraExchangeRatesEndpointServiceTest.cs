using ExchangeRates.Application.Services;
using ExchangeRates.Domain.Interfaces.Logger;
using ExchangeRates.Domain.Validations;
using Microsoft.Extensions.Configuration;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System.Collections.Generic;

namespace ExchangeRates.Test.Application.Services
{
   [TestClass]
   public class ConfiguraExchangeRatesEndpointServiceTest
   {
      private readonly IConfiguration _configuration;
      private readonly IConfiguration _configurationEmpty;

      public ConfiguraExchangeRatesEndpointServiceTest()
      {
         var myConfiguration = new Dictionary<string, string>
            {
                {"API:ExchangeRates:AccessKey", "67a6468abe7b02910fc369186842142c"},
                {"API:ExchangeRates:v1", "http://api.exchangeratesapi.io/v1/"}
            };

         _configuration = new ConfigurationBuilder()
             .AddInMemoryCollection(myConfiguration)
             .Build();

         _configurationEmpty = new ConfigurationBuilder()
             .Build();
      }

      [TestMethod]
      public void Given_A_Valid_FromCurrency_And_A_Valid_ToCurrency_But_Without_Configuration_Should_Return_Null_Endpoint()
      {
         //Arrange
         var mockCustomLogger = new Mock<ICustomLogger>();
         var customLogger = mockCustomLogger.Object;

         var mockCustomValidator = new Mock<ICustomValidator>();
         var customValidator = mockCustomValidator.Object;

         string fromCurrrency = "EUR";
         string toCurrrency = "USD";

         ConfigureExchangeRatesEndpointService configuraExchangeRatesEndpointService = new ConfigureExchangeRatesEndpointService(
            _configurationEmpty, 
            customLogger, 
            customValidator);

         //Act
         string endpoint = configuraExchangeRatesEndpointService.Configure(fromCurrrency, toCurrrency);

         //Assert
         Assert.IsNull(endpoint);
      }
           
      [TestMethod]
      public void Given_A_Valid_FromCurrency_And_A_Valid_ToCurrency_Should_Return_Endpoint()
      {
         //Arrange
         var mockCustomLogger = new Mock<ICustomLogger>();
         var customLogger = mockCustomLogger.Object;

         var mockCustomValidator = new Mock<ICustomValidator>();
         var customValidator = mockCustomValidator.Object;

         string fromCurrrency = "EUR";
         string toCurrrency = "USD";

         ConfigureExchangeRatesEndpointService configuraExchangeRatesEndpointService = new ConfigureExchangeRatesEndpointService(_configuration, customLogger, customValidator);

         //Act
         string endpoint = configuraExchangeRatesEndpointService.Configure(fromCurrrency, toCurrrency);

         //Assert
         Assert.IsNotNull(endpoint);
      }
   }
}
