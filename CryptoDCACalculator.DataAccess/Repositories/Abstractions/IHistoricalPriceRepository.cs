using CryptoDCACalculator.DataAccess.Entities;
using LearningPlatform.DataAccess.Repositories.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CryptoDCACalculator.DataAccess.Repositories.Abstractions
{
    public interface IHistoricalPriceRepository : IGenericRepository<HistoricalPrice>
    {
        Task<HistoricalPrice> GetPriceByNameAndDateAsync(string cryptoCurrencyName, DateTime date);
    }
}
