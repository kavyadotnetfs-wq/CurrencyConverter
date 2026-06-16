# Currency Converter API

A lightweight ASP.NET Core 8 Web API that converts monetary amounts between **USD**, **INR**, and **EUR** using exchange rates stored in a local JSON file.

---

## Prerequisites
[.NET SDK](https://dotnet.microsoft.com/download) | 8.0 or later |

Verify with:
```bash
dotnet --version   # should print 8.x.x
```

---

## Running locally

# 1. Clone / unzip the project
cd CurrencyConverter.API

# 2. Restore & run
dotnet run
```
The API starts on `http://localhost:5018` (and `https://localhost:5018`).  
---

## Endpoints

### `GET /convert`
This request converts the source currency to targeted currency values with the exchange rate
**Example requests**
# USD → INR
curl(GET) "http://localhost:5000/convert?sourceCurrency=USD&targetCurrency=INR&amount=100"

# EUR → USD
curl "http://localhost:5000/convert?sourceCurrency=EUR&targetCurrency=USD&amount=50"

**Success response (200 OK)**
```json
{
  "exchangeRate": 74.00,
  "convertedAmount": 7400.0000
}
```
