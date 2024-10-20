using CryptoDCACalculator.DataAccess.Context;
using CryptoDCACalculator.DataAccess.Entities;
using CryptoDCACalculator.DataAccess.Repositories.Abstractions;
using LearningPlatform.DataAccess.Repositories.Abstractions;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CryptoDCACalculator.DataAccess.Repositories
{
    public class HistoricalPriceRepository : GenericRepository<HistoricalPrice>, IHistoricalPriceRepository
    {
        public HistoricalPriceRepository(CryptoDCACalculatorContext context) : base(context)
        {
        }

        public async Task<HistoricalPrice> GetPriceByNameAndDateAsync(string cryptoCurrencyName, DateTime date)
        {
            return await context.HistoricalPrices
                .FirstOrDefaultAsync(p => p.CryptoCurrencyName == cryptoCurrencyName && p.Date == date);
        }
    }
}