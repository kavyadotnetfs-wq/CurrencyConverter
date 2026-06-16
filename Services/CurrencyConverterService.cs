using CurrencyConverter.Models;
using System.Text.Json;

namespace CurrencyConverter.Services
{
    public class CurrencyConverterService: ICurrencyConverterService
    {
        private readonly ILogger<CurrencyConverterService> _logger;
        private readonly IConfiguration _config;

        public CurrencyConverterService(IConfiguration config,ILogger<CurrencyConverterService> logger)
        {
            _logger = logger;
            _config = config;
        }
        
        public async Task<CurrencyDetails> Convert(string sourceCurrency, string targetCurrency, decimal amount)
        {
            try
            {
                _logger.LogInformation("Entered into CurrencyConverterService.");
                var key = string.Format("{0}_TO_{1}",
             sourceCurrency.Trim('"').ToUpper(),
             targetCurrency.Trim('"').ToUpper());

                var value = _config[key];
                decimal.TryParse(value, out var rate);
                if (rate > 0)
                {
                    var response = new CurrencyDetails()
                    {
                        ExchangeRate = rate,
                        ConvertedAmount = amount * rate
                    };                    
                    return response;
                }
                _logger.LogInformation("Value from ExchangeRates.json is less than 0");
            }
            catch(Exception ex)
            {
                _logger.LogError("Exception occured in CurrencyConverterController's CurrencyConverterService's Convert Method", ex.Message);   
            }
            
            return null;
         }
    }
}
