using CryptoDCACalculator.DataAccess.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CryptoDCACalculator.Business.Services.Abstractions
{
    public interface IHistoricalPriceService
    {
        Task<HistoricalPrice> GetPriceForDateByNameAsync(string cryptoCurrencyName, DateTime date);
        Task<decimal> GetCurrentPriceAsync(string cryptoCurrencyName);
        Task<decimal> GetCachedCurrentPriceAsync(string cryptoCurrencyName);
    }
}
