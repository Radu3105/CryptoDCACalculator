using CryptoDCACalculator.DataAccess.Entities.Abstractions;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CryptoDCACalculator.DataAccess.Entities
{
    public class HistoricalPrice : GenericEntity
    {
        public Guid CryptoCurrencyId { get; set; }
        public string CryptoCurrencyName { get; set; }
        public DateTime Date { get; set; }
        [Column(TypeName = "decimal(18,2)")]
        public decimal Price { get; set; }
    }
}
