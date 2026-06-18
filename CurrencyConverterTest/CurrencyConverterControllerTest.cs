using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Moq;
using CurrencyConverter.Controllers;
using CurrencyConverter.Models;
using CurrencyConverter.Services;
using Microsoft.Extensions.Logging;
using Castle.Core.Logging;
using Microsoft.AspNetCore.Mvc;

namespace CurrencyConverterTest
{
    public class CurrencyConverterControllerTest
    {
        private readonly Mock<ICurrencyConverterService> _service;
        private readonly Mock<ILogger<CurrencyConverterController>> _logger;

        public CurrencyConverterControllerTest()
        {
            _service = new Mock<ICurrencyConverterService>();
            _logger  = new Mock<ILogger<CurrencyConverterController>>();
        }
        [Fact]
        public async Task Covert_Success_flow()
        {
            var currencyController = new CurrencyConverterController(_service.Object, _logger.Object);
                       
            var converted_amt = new CurrencyDetails()
            {
                ExchangeRate = 2,
                ConvertedAmount = 2
            };
            var res = _service.Setup(s => s.Convert(It.IsAny<string>(),It.IsAny<string>(),It.IsAny<Decimal>()))
            .ReturnsAsync(converted_amt);
            var controllerConvert =await currencyController.Convert("USD", "INR", 2);
            var result = Assert.IsType<OkObjectResult>(controllerConvert);
            Assert.Equal(200, result.StatusCode);
            
        }
        [Fact]
        public async Task Convert_SourceCurrencyIsNullOrWhiteSpace_Return400BadRequestError()
        {
            var currencyController = new CurrencyConverterController(_service.Object,_logger.Object);
            var controller_Result =await currencyController.Convert("", "INR", 2);
            var result = Assert.IsType<BadRequestResult>(controller_Result);
            Assert.Equal(400, result.StatusCode);
        }
        [Fact]
        public async Task Convert_TargetCurrencyIsNullOrWhiteSpace_Return400BadRequestError()
        {
            var currencyController = new CurrencyConverterController(_service.Object, _logger.Object);
            var controller_Result = await currencyController.Convert("USD", "", 2);
            var result = Assert.IsType<BadRequestResult>(controller_Result);
            Assert.Equal(400, result.StatusCode);
        }
        [Fact]
        public async Task Convert_AmountIsValidOrNot_Return400BadRequestError()
        {
            var currencyController = new CurrencyConverterController(_service.Object, _logger.Object);
            var controller_Result = await currencyController.Convert("USD", "INR", 0);
            var result = Assert.IsType<BadRequestResult>(controller_Result);
            Assert.Equal(400, result.StatusCode);

        }
        [Fact]
        public async Task Convert_SourceCurrencyCharLengthCheck_Return400BadRequestError()
        {
            var currencyController = new CurrencyConverterController(_service.Object, _logger.Object);
            var controller_Result = await currencyController.Convert("USDDD", "INR", 2);
            var result = Assert.IsType<BadRequestResult>(controller_Result);
            Assert.Equal(400,result.StatusCode);
        }
        [Fact]
        public async Task Convert_SourceCurrencyCharCheck_Return400BadRequestError()
        {
            var currencyController = new CurrencyConverterController(_service.Object, _logger.Object);
            var controller_Result = await currencyController.Convert("USD123", "INR", 2);
            var result = Assert.IsType<BadRequestResult>(controller_Result);
            Assert.Equal(400, result.StatusCode);
        }
        [Fact]
        public async Task Convert_TargetCurrencyCharLengthCheck_Return400BadRequestError()
        {
            var currencyController = new CurrencyConverterController(_service.Object, _logger.Object);
            var controller_Result = await currencyController.Convert("USD", "INRRRR", 2);
            var result = Assert.IsType<BadRequestResult>(controller_Result);
            Assert.Equal(400, result.StatusCode);
        }
        [Fact]
        public async Task Convert_TargetCurrencyCharCheck_Return400BadRequestError()
        {
            var currencyController = new CurrencyConverterController(_service.Object, _logger.Object);
            var controller_Result = await currencyController.Convert("USD", "INR123", 2);
            var result = Assert.IsType<BadRequestResult>(controller_Result);
            Assert.Equal(400, result.StatusCode);
        }
        [Fact]
        public async Task Convert_SourceCurrencyIsEqualstoTargetCurrencyCheck_Return400BadRequestError()
        {
            var currencyController = new CurrencyConverterController(_service.Object, _logger.Object);
            var controller_Result = await currencyController.Convert("USD", "USD", 2);
            var result = Assert.IsType<BadRequestResult>(controller_Result);
            Assert.Equal(400, result.StatusCode);
        }
    }
}
