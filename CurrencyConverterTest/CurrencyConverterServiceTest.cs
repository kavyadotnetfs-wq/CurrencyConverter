using CurrencyConverter.Controllers;
using CurrencyConverter.Models;
using CurrencyConverter.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Configuration;
using Moq;

namespace CurrencyConverterTest
{
    public class CurrencyConverterServiceTest
    {
        private readonly Mock<ILogger<CurrencyConverterService>> _logger;
        private readonly Mock<IConfiguration> _config;

        public CurrencyConverterServiceTest()
        {
            _logger = new Mock<ILogger<CurrencyConverterService>>();
            _config = new Mock<IConfiguration>();
        }

        [Fact]
        public async Task Convert_Service_SuccessFlow()
        {
            var currencyService = new CurrencyConverterService(_config.Object, _logger.Object);
            _config.Setup(c => c["INR_TO_USD"]).Returns("0.013"); 
            var result = await currencyService.Convert("INR", "USD", 1);
            Assert.True(result.ConvertedAmount > 0);
        }
        [Fact]
        public async Task Convert_Service_Failure_Flow()
        {
            var currencyService = new CurrencyConverterService(_config.Object, _logger.Object);
            _config.Setup(c => c["INR_TO_USD"]).Returns("-0.013");
            var result = await currencyService.Convert("INR", "USD", 1);
            Assert.True(result.ConvertedAmount <= 0);
        }
    }
}
