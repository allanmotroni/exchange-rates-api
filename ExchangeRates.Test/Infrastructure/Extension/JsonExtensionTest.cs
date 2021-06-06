using ExchangeRates.Infrastructure.Dto;
using ExchangeRates.Infrastructure.Extension;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ExchangeRates.Test.Infrastructure.Extension
{
    [TestClass]
    public class JsonExtensionTest
    {
        [TestMethod]
        public void Given_A_Valid_ExchangeRatesDto_Json_String_Should_Convert_To_ExchangeRatesDto()
        {
            //Arrange
            string exchangeRatesDtoJson = "{\"success\": true,\"timestamp\": 1623010457,\"base\": \"EUR\",\"date\": \"2021-06-06\",\"rates\": {\"USD\": 1.21633}}";

            //Act
            ExchangeRatesDto exchangeRatesDto = exchangeRatesDtoJson.ToClassOf<ExchangeRatesDto>();

            //Assert
            Assert.IsNotNull(exchangeRatesDto);            
        }

        [TestMethod]
        public void Given_A_Invalid_ExchangeRatesDto_Json_String_Should_Convert_To_Null()
        {
            //Arrange
            string exchangeRatesDtoJson = "";

            //Act
            ExchangeRatesDto exchangeRatesDto = exchangeRatesDtoJson.ToClassOf<ExchangeRatesDto>();

            //Assert
            Assert.IsNull(exchangeRatesDto);
        }
    }
}
