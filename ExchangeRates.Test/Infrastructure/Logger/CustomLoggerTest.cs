using ExchangeRates.Domain.Interfaces.Logger;
using ExchangeRates.Infrastructure.Logger;
using Microsoft.Extensions.Logging;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;

namespace ExchangeRates.Test.Infrastructure.Logger
{
    [TestClass]
    public class CustomLoggerTest
    {
        [TestMethod]
        public void Given_A_Message_Should_Write_A_Info_Log()
        {
            //Arrange
            string message = "Info message log";

            var mockLogger = new Mock<ILogger<CustomLogger>>();
            var logger = mockLogger.Object;

            ICustomLogger customLogger = new CustomLogger(logger);

            //Act
            customLogger.Info(message);

            //Assert
            mockLogger.Verify(log => log.Log(LogLevel.Information, It.IsAny<EventId>(), It.Is<It.IsAnyType>((v, t) => true), null, It.Is<Func<It.IsAnyType, Exception, string>>((v, t) => true)), Times.Once);
        }

        [TestMethod]
        public void Given_A_Message_Should_Write_A_Warn_Log()
        {
            //Arrange
            string message = "Warn message log";

            var mockLogger = new Mock<ILogger<CustomLogger>>();
            var logger = mockLogger.Object;

            ICustomLogger customLogger = new CustomLogger(logger);

            //Act
            customLogger.Warn(message);

            //Assert
            mockLogger.Verify(log => log.Log(LogLevel.Warning, It.IsAny<EventId>(), It.Is<It.IsAnyType>((v, t) => true), null, It.Is<Func<It.IsAnyType, Exception, string>>((v, t) => true)), Times.Once);
        }

        [TestMethod]
        public void Given_A_Message_Should_Write_A_Error_Log()
        {
            //Arrange
            string message = "Error message log";

            var mockLogger = new Mock<ILogger<CustomLogger>>();
            var logger = mockLogger.Object;

            ICustomLogger customLogger = new CustomLogger(logger);

            //Act
            customLogger.Error(message);

            //Assert
            mockLogger.Verify(log => log.Log(LogLevel.Error, It.IsAny<EventId>(), It.Is<It.IsAnyType>((v, t) => true), null, It.Is<Func<It.IsAnyType, Exception, string>>((v, t) => true)), Times.Once);
        }

        [TestMethod]
        public void Given_A_Message_Should_Write_A_Exception_And_Message_Log()
        {
            //Arrange
            string message = "Exception message log";

            var mockLogger = new Mock<ILogger<CustomLogger>>();
            var logger = mockLogger.Object;

            ICustomLogger customLogger = new CustomLogger(logger);

            //Act
            customLogger.Exception(message, new Exception("Exception error"));

            //Assert
            mockLogger.Verify(log => log.Log(LogLevel.Critical, It.IsAny<EventId>(), It.Is<It.IsAnyType>((v, t) => true), It.IsAny<Exception>(), It.Is<Func<It.IsAnyType, Exception, string>>((v, t) => true)), Times.Once);
        }

        [TestMethod]
        public void Given_A_Message_Should_Write_Only_A_Exception_Log()
        {
            //Arrange
            var mockLogger = new Mock<ILogger<CustomLogger>>();
            var logger = mockLogger.Object;

            ICustomLogger customLogger = new CustomLogger(logger);

            //Act
            customLogger.Exception(new Exception("Exception error"));

            //Assert
            mockLogger.Verify(log => log.Log(LogLevel.Critical, It.IsAny<EventId>(), It.Is<It.IsAnyType>((v, t) => true), It.IsAny<Exception>(), It.Is<Func<It.IsAnyType, Exception, string>>((v, t) => true)), Times.Once);
        }
    }
}
