using CurrencyConverter.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace CurrencyConverter.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CurrencyConverterController: Controller
    {
        private readonly ICurrencyConverterService _service;
        private readonly ILogger<CurrencyConverterController> _logger;
        public CurrencyConverterController(ICurrencyConverterService service, ILogger<CurrencyConverterController> logger)
        {
            _service = service;
            _logger = logger;
        }

        [HttpGet("convert")]
        public async Task<ActionResult> Convert([FromQuery]string s_Currency, [FromQuery] string t_Currency, [FromQuery] decimal amount)
        {
            string sourceCurrency = s_Currency.Trim().Trim('"');
            string targetCurrency = t_Currency.Trim().Trim('"');

            if (string.IsNullOrWhiteSpace(sourceCurrency))
            {
                _logger.LogWarning("Source Currency is Empty");
                return BadRequest();
            }                
            if (string.IsNullOrWhiteSpace(targetCurrency))
            {
                _logger.LogWarning("Target Currency is Empty");
                return BadRequest();
            }
                
            if (amount <= 0)
            {
                _logger.LogWarning("Invalid amount is received");
                return BadRequest();
            }               
            if (sourceCurrency.Length != 3 || !sourceCurrency.All(char.IsLetter))
            {
                _logger.LogWarning("Source Currency value is not valid");
                return BadRequest();
            }
                
            if (targetCurrency.Length != 3 || !targetCurrency.All(char.IsLetter))
            {
                _logger.LogWarning("Target Currency value is not valid");
                return BadRequest();
            }                
            if (sourceCurrency.Trim().ToUpper() == targetCurrency.Trim().ToUpper())
            {
                _logger.LogWarning("Source Currency and Target Currency values are same");
                return BadRequest();
            }
                

            try
            {                 
                var result = await _service.Convert(sourceCurrency, targetCurrency, amount);
                if (result != null)
                {
                    _logger.LogInformation("Successfully converted from Source Currency to Target Currency");
                    return Ok(result);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError("Can not Successfully converted from Source Currency to Target Currency",ex.Message);                
            }          
            
            return NotFound();
        }

    }
}


