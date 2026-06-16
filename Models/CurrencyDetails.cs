using CurrencyConverter.Services;
using Microsoft.AspNetCore.Mvc;

namespace CurrencyConverter.Models
{
    public class CurrencyDetails
    {
        public decimal ExchangeRate { get; set; }
        public decimal ConvertedAmount { get; set; }
    }
}

 