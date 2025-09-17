using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TravelApp.Domain.DTOs
{
    public class CountryInfoDetailsDto
    {
        public string Name { get; set; }
        public string Capital { get; set; }
        public string Region { get; set; }
        public string Currency {  get; set; }
        public string Phonecode { get; set; }
    }
}
