using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TravelApp.Domain.DomainModels
{
    public class City : BaseEntity
    {
        public string? Name { get; set; }
        public Guid CountryId { get; set; }
        public Country? Country { get; set; }
        public virtual ICollection<Travel>? Travels { get; set; }
    }
}
