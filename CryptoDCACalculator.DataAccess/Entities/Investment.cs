using CryptoDCACalculator.DataAccess.Entities.Abstractions;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CryptoDCACalculator.DataAccess.Entities
{
    public class Investment : GenericEntity
    {
        public Guid CurrencyId { get; set; }
        public CryptoCurrency Currency { get; set; }
        public DateTime Date { get; set; }
        [Column(TypeName = "decimal(18,2)")]
        public decimal InvestedAmount { get; set; }
        [Column(TypeName = "decimal(18,2)")]
        public decimal CurrencyAmount { get; set; }
        [Column(TypeName = "decimal(18,2)")]
        public decimal CurrentValue { get; set; }
    }
}
