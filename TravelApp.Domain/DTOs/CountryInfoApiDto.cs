using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TravelApp.Domain.DTOs
{
    public class CountryInfoApiDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Iso3 { get; set; }
        public string NumericCode { get; set; }
        public string Iso2 { get; set; }
        public string Phonecode { get; set; }
        public string Capital { get; set; }
        public string Currency { get; set; }
        public string Currency_Name { get; set; }
        public string Currency_Symbol { get; set; }
        public string Tld { get; set; }
        public string Native { get; set; }
        public string Region { get; set; }
        public int RegionId { get; set; }
        public string Subregion { get; set; }
        public int SubregionId { get; set; }
        public string Nationality { get; set; }
        public string Timezones { get; set; }
        public string Translations { get; set; }
        public string Latitude { get; set; }
        public string Longitude { get; set; }
        public string Emoji { get; set; }
        public string EmojiU { get; set; }
    }
}
