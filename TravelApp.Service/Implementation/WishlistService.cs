using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TravelApp.Domain.DomainModels;
using TravelApp.Domain.Identity;
using TravelApp.Repository;
using TravelApp.Service.Interface;

namespace TravelApp.Service.Implementation
{
    public class WishlistService : IWishlistService
    {
        private readonly IRepository<TravelWishlist> _wishlistRepository;
        private readonly UserManager<TravelAppUser> _userManager;
        private readonly ITravelService _travelService;

        public WishlistService(IRepository<TravelWishlist> wishlistRepository, UserManager<TravelAppUser> userManager, ITravelService travelService)
        {
            _wishlistRepository = wishlistRepository;
            _userManager = userManager;
            _travelService = travelService;
        }

        private async Task<TravelAppUser?> GetUserByIdAsync(string userId)
        {
            return await _userManager.FindByIdAsync(userId);
        }

        public TravelWishlist AddToWishlist(Guid travelId, string userId)
        {
            var travel = _travelService.GetById(travelId);
            var user = GetUserByIdAsync(userId).Result;

            if (travel != null && user != null)
            {

                return _wishlistRepository.Insert(
                    new TravelWishlist
                    {
                        Id = new Guid(),
                        TravelAppUserId = userId,
                        TravelAppUser = user,
                        TravelId = travelId,
                        Travel = travel
                    });
            }
            return null;
        }

        public List<TravelWishlist> GetUserWishlist(string userId)
        {
            return _wishlistRepository.GetAll(selector: x => x,
                predicate: x => x.TravelAppUserId == userId).ToList();
        }

        public List<Travel> GetUserWishlistTravels(string userId)
        {
            var travelIds = GetUserWishlist(userId).Select(w => w.TravelId).ToList();

            var travels = _travelService.GetByIdsWithCity(travelIds);

            return travels;
        }

        public bool IsInWishlist(Guid travelId, string userId)
        {
            return _wishlistRepository.Get(selector: x => x,
                predicate: x => x.TravelId == travelId && x.TravelAppUserId == userId) == null ? false : true;
        }
        public bool RemoveFromWishlist(Guid travelId, string userId)
        {
            var wishlistItem = _wishlistRepository.Get(
                selector: x => x,
                predicate: x => x.TravelId == travelId && x.TravelAppUserId == userId);

            if (wishlistItem != null)
            {
                _wishlistRepository.Delete(wishlistItem);
                return true;
            }
            return false;
        }
    }
}
