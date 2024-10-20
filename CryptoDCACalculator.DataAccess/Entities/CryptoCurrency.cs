using CryptoDCACalculator.DataAccess.Entities.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CryptoDCACalculator.DataAccess.Entities
{
    public class CryptoCurrency : GenericEntity
    {
        public string Name { get; set; }
        public string Symbol { get; set; }
    }
}
