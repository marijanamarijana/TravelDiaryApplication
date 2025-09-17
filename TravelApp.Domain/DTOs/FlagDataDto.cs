using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TravelApp.Domain.DTOs
{
    public class FlagDataDto
    {
        public string Name { get; set; }
        public string Iso2 { get; set; }
        public string Iso3 { get; set; }
        public string Flag { get; set; }   // URL
    }
}
