using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CryptoDCACalculator.Business.Services.Abstractions
{
    public interface ICryptoApiService
    {
        Task<decimal> GetPriceFromApiAsync(string cryptoCurrencyName, DateTime date);
        Task<decimal> GetCurrentPriceFromApiAsync(string cryptoCurrencyName);
    }
}
