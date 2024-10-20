using CryptoDCACalculator.DataAccess.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace CryptoDCACalculator.DataAccess.Context
{
    public class CryptoDCACalculatorContext : DbContext
    {
        public CryptoDCACalculatorContext(DbContextOptions<CryptoDCACalculatorContext> options) : base(options)
        {
        }

        public DbSet<CryptoCurrency> CryptoCurrencies { get; set; }
        public DbSet<Investment> Investments { get; set; }
        public DbSet<HistoricalPrice> HistoricalPrices { get; set; }
    }
}
