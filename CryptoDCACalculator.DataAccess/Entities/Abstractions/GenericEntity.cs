using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CryptoDCACalculator.DataAccess.Entities.Abstractions
{
    public abstract class GenericEntity
    {
        [Key]
        public Guid Id { get; set; }
    }
}
