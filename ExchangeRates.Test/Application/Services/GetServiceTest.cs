using ExchangeRates.Application.Services;
using ExchangeRates.Domain.Interfaces.Logger;
using ExchangeRates.Domain.Validations;
using Microsoft.Extensions.Configuration;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ExchangeRates.Test.Application.Services
{

   [TestClass]
   public class GetServiceTest
   {
      private readonly IConfiguration _configuration;
      private readonly string _baseUrl;
      private readonly string _accessKey;

      public GetServiceTest()
      {
         var myConfiguration = new Dictionary<string, string>
            {
                {"API:ExchangeRates:AccessKey", "67a6468abe7b02910fc369186842142c"},
                {"API:ExchangeRates:v1", "http://api.exchangeratesapi.io/v1/"}
            };

         _configuration = new ConfigurationBuilder()
             .AddInMemoryCollection(myConfiguration)
             .Build();

         _baseUrl = _configuration["API:ExchangeRates:v1"];
         _accessKey = _configuration["API:ExchangeRates:AccessKey"];
      }

      [TestMethod]
      public async Task Given_A_Valid_Endpoint_Should_Return_Json_From_Get()
      {
         //Arrange
         var mockCustomLogger = new Mock<ICustomLogger>();
         var mockCustomValidator = new Mock<ICustomValidator>();

         var configureEndpoint = new ConfigureExchangeRatesEndpointService(
            _configuration,
            mockCustomLogger.Object,
            mockCustomValidator.Object);

         string fromCurrency = "EUR";
         string toCurrency = "BRL";
         string endpoint = configureEndpoint.Configure(fromCurrency, toCurrency);

         var getService = new GetService(mockCustomValidator.Object, mockCustomLogger.Object);

         //Act
         var json = await getService.GetAsync(endpoint);

         //Assert
         Assert.IsNotNull(json);
      }

      [TestMethod]
      public async Task Given_A_Valid_Endpoint_Should_Return_A_Validation()
      {
         //Arrange
         var mockCustomLogger = new Mock<ICustomLogger>();
         var mockCustomValidator = new Mock<ICustomValidator>();

         var configureEndpoint = new ConfigureExchangeRatesEndpointService(
            _configuration,
            mockCustomLogger.Object,
            mockCustomValidator.Object);

         string fromCurrency = "BRL";
         string toCurrency = "USD";
         string endpoint = configureEndpoint.Configure(fromCurrency, toCurrency);

         ICustomValidator customValidator = new CustomValidator();
         var getService = new GetService(customValidator, mockCustomLogger.Object);

         //Act
         var json = await getService.GetAsync(endpoint);

         //Assert
         Assert.IsTrue(customValidator.HasErrors());
      }

      [TestMethod]
      public async Task Given_An_Empty_Endpoint_Should_Return_Null()
      {
         //Arrange
         var mockCustomLogger = new Mock<ICustomLogger>();
         var mockCustomValidator = new Mock<ICustomValidator>();

         string endpoint = string.Empty;

         var getService = new GetService(mockCustomValidator.Object, mockCustomLogger.Object);

         //Act
         var json = await getService.GetAsync(endpoint);

         //Assert
         Assert.IsNull(json);
      }
   }
}
