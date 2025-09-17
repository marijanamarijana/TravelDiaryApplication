using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TravelApp.Domain.DTOs;

namespace TravelApp.Service.Interface
{
    public interface IApiService
    {
        Task<CountryInfoDetailsDto?> GetCountryInfoAsync(string iso2);
        Task<FlagDataDto> GetCountryFlagAsync(string iso2);
    }
}
