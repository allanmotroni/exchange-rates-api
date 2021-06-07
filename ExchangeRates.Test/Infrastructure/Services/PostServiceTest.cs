using ExchangeRates.Domain.Interfaces.Logger;
using ExchangeRates.Domain.Validations;
using ExchangeRates.Infrastructure.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ExchangeRates.Test.Infrastructure.Services
{

    [TestClass]
    public class PostServiceTest
    {
        private readonly IConfiguration _configuration;
        public PostServiceTest()
        {
            var myConfiguration = new Dictionary<string, string>
            {
                {"API:ExchangeRates:AccessKey", "67a6468abe7b02910fc369186842142c"},
                {"API:ExchangeRates:v1", "http://api.exchangeratesapi.io/v1/"}
            };

            _configuration = new ConfigurationBuilder()
                .AddInMemoryCollection(myConfiguration)
                .Build();
        }

        [TestMethod]
        public async Task Given_A_Valid_Endpoint_Should_Return_Json_From_Post()
        {
            //Arrange
            var mockCustomLogger = new Mock<ICustomLogger>();
            var mockCustomValidator = new Mock<ICustomValidator>();

            var configureEndpoint = new ConfigureExchangeRatesEndpointService(_configuration, mockCustomLogger.Object, mockCustomValidator.Object);

            string fromCurrency = "EUR";
            string toCurrency = "BRL";
            string endpoint = configureEndpoint.Configure(fromCurrency, toCurrency);

            var postService = new PostService(mockCustomValidator.Object, mockCustomLogger.Object);

            //Act
            var json = await postService.Post(endpoint);

            //Assert
            Assert.IsNotNull(json);
        }

        [TestMethod]
        public async Task Given_An_Invalid_Endpoint_Should_Return_Null()
        {
            //Arrange
            var mockCustomLogger = new Mock<ICustomLogger>();
            var mockCustomValidator = new Mock<ICustomValidator>();

            string endpoint = string.Empty;

            var postService = new PostService(mockCustomValidator.Object, mockCustomLogger.Object);

            //Act
            var json = await postService.Post(endpoint);

            //Assert
            Assert.IsNull(json);
        }
    }
}
