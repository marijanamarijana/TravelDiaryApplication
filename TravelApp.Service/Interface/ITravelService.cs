using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TravelApp.Domain.DomainModels;

namespace TravelApp.Service.Interface
{
    public interface ITravelService
    {
        List<Travel> GetAll();
        Travel? GetById(Guid Id);
        Travel Update(Travel travel, List<Guid> cityIds);
        Travel DeleteById(Guid Id);
        Travel Add(Travel travel);
        List<Travel> GetByIdsWithCity(List<Guid> travelIds);
        // Travel AddToPastTravels(Travel travel)
    }
}
