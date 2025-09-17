using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TravelApp.Domain.DomainModels;

namespace TravelApp.Service.Interface
{
    public interface IWishlistService
    {
        TravelWishlist AddToWishlist(Guid travelId, string userId);
        List<TravelWishlist> GetUserWishlist(string userId);
        List<Travel?> GetUserWishlistTravels(string userId);
        bool IsInWishlist(Guid travelId, string userId);
        bool RemoveFromWishlist(Guid travelId, string userId);
    }
}
