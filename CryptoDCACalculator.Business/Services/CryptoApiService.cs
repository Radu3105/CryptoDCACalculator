using CryptoDCACalculator.Business.Services.Abstractions;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CryptoDCACalculator.Business.Services
{
    public class CryptoApiService : ICryptoApiService
    {
        private readonly HttpClient _httpClient;

        public CryptoApiService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<decimal> GetPriceFromApiAsync(string cryptoCurrencyName, DateTime date)
        {
            using (HttpClient client = new HttpClient())
            {
                string formattedDate = date.ToString("dd-MM-yyyy");
                string apiUrl = $"https://api.coingecko.com/api/v3/coins/{cryptoCurrencyName}/history?date={formattedDate}&vs_currencies=eur&precision=2";
                var response = await client.GetStringAsync(apiUrl);
                var json = JObject.Parse(response);
                decimal price = json["market_data"]["current_price"]["eur"].Value<decimal>();
                return price;
            }
        }
        
        public async Task<decimal> GetCurrentPriceFromApiAsync(string cryptoCurrencyName)
        {
            Console.WriteLine(cryptoCurrencyName);
            using (HttpClient client = new HttpClient())
            {
                string apiUrl = $"https://api.coingecko.com/api/v3/simple/price?ids={cryptoCurrencyName}&vs_currencies=eur&precision=2";
                var response = await client.GetStringAsync(apiUrl);
                Console.WriteLine(response);  // Log the raw JSON response for debugging
                var json = JObject.Parse(response);
                decimal currentPrice = json[cryptoCurrencyName]["eur"].Value<decimal>();
                return currentPrice;
            }
        }
    }
}
