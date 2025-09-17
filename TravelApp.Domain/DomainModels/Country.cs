using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TravelApp.Domain.DomainModels
{
    public class Country : BaseEntity
    {
        public string? Name { get; set; }
        public string ISO2Code { get; set; }
        public virtual ICollection<City>? Cities { get; set; }
    }
}
