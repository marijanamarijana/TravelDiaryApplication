 using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TravelApp.Domain.DomainModels;

namespace TravelApp.Service.Interface
{
    public interface IPastTravelsService
    {
        TravelBooking AddToPastTravels(Guid travelId, string userId);
        List<TravelBooking> GetUserPastTravels(string userId);
        List<Travel> GetUserPastTravelsObjects(string userId);
        bool IsInPastTravels(Guid travelId, string userId);
        bool RemoveFromPastTravels(Guid travelId, string userId);
    }
}
