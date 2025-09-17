using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TravelApp.Domain.DomainModels;

namespace TravelApp.Service.Interface
{
    public interface ICountryService
    {
        List<Country> GetAll();
        Country? GetById(Guid Id);
        Country Update(Country country);
        Country DeleteById(Guid Id);
        Country Add(Country country);
    }
}
