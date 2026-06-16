using CurrencyConverter.Models;

namespace CurrencyConverter.Services
{
    public interface ICurrencyConverterService
    {
        public Task<CurrencyDetails> Convert(string sourceCurrency,string targetCurrency,decimal amount);
    }
}
