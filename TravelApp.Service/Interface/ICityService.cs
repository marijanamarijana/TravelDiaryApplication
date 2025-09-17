using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TravelApp.Domain.DomainModels;

namespace TravelApp.Service.Interface
{
    public interface ICityService
    {
        List<City> GetAll();
        City? GetById(Guid Id);
        City Update(City city);
        City DeleteById(Guid Id);
        City Add(City city);
    }
}
