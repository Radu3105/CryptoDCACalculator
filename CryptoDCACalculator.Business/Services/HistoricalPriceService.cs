using CryptoDCACalculator.Business.Services.Abstractions;
using CryptoDCACalculator.DataAccess.Entities;
using CryptoDCACalculator.DataAccess.Repositories.Abstractions;
using Microsoft.Extensions.Caching.Memory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CryptoDCACalculator.Business.Services
{
    public class HistoricalPriceService : IHistoricalPriceService
    {
        private readonly IHistoricalPriceRepository _historicalPriceRepository;
        private readonly ICryptoApiService _cryptoApiService;
        private static readonly MemoryCache _cache = new MemoryCache(new MemoryCacheOptions());
        
        public HistoricalPriceService(IHistoricalPriceRepository historicalPriceRepository, ICryptoApiService cryptoApiService)
        {
            _historicalPriceRepository = historicalPriceRepository;
            _cryptoApiService = cryptoApiService;
        }

        public async Task<decimal> GetCurrentPriceAsync(string cryptoCurrencyName)
        {
            var response = await _cryptoApiService.GetCurrentPriceFromApiAsync(cryptoCurrencyName);
            return response;
        }

        public async Task<decimal> GetCachedCurrentPriceAsync(string cryptoCurrencyName)
        {
            // Check if the cache contains the current cryptocurrency name
            if (!_cache.TryGetValue(cryptoCurrencyName, out decimal currentPrice))
            {
                // Get the current cryptocurrency price
                currentPrice = await _cryptoApiService.GetCurrentPriceFromApiAsync(cryptoCurrencyName);

                // Cache the current cryptocurrency name
                _cache.Set(cryptoCurrencyName, currentPrice, TimeSpan.FromMinutes(5));
            }
            return currentPrice;
        }

        public async Task<HistoricalPrice> GetPriceForDateByNameAsync(string cryptoCurrencyName, DateTime date)
        {
            // Get the price by cryptocurrency name and date from the database
            var price = await _historicalPriceRepository.GetPriceByNameAndDateAsync(cryptoCurrencyName, date);
            
            // If the price is not in the database yet, make an api request and store it in the database
            if (price == null)
            {
                decimal apiPrice = await _cryptoApiService.GetPriceFromApiAsync(cryptoCurrencyName, date);
                //await Task.Delay(2200); // Add delay of 2.2 seconds between each api request (max 30 calls / minute)
                price = new HistoricalPrice
                {
                    CryptoCurrencyName = cryptoCurrencyName,
                    Date = date,
                    Price = apiPrice
                };
                await _historicalPriceRepository.AddAsync(price);
            }
            return price;
        }
    }
}
