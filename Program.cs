using CurrencyConverter.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddSingleton<ICurrencyConverterService, CurrencyConverterService>();
builder.Configuration
       .AddJsonFile(
           "exchangeRates.json",
           optional: false,
           reloadOnChange: true).AddEnvironmentVariables();
var app = builder.Build();


app.UseAuthorization();

app.MapControllers();

app.Run();
