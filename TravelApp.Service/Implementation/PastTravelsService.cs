using Microsoft.AspNetCore.Identity;
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
    public class PastTravelsService : IPastTravelsService
    {
        private readonly IRepository<TravelBooking> _pastTravelRepository;
        private readonly UserManager<TravelAppUser> _userManager;
        private readonly ITravelService _travelService;

        public PastTravelsService(IRepository<TravelBooking> pastTravelRepository, UserManager<TravelAppUser> userManager, ITravelService travelService)
        {
            _pastTravelRepository = pastTravelRepository;
            _userManager = userManager;
            _travelService = travelService;
        }
        private async Task<TravelAppUser?> GetUserByIdAsync(string userId)
        {
            return await _userManager.FindByIdAsync(userId);
        }
        public TravelBooking AddToPastTravels(Guid travelId, string userId)
        {
            var travel = _travelService.GetById(travelId);
            var user = GetUserByIdAsync(userId).Result;

            if (travel != null && user != null)
            {

                return _pastTravelRepository.Insert(
                    new TravelBooking
                    {
                        Id = new Guid(),
                        BookerId = userId,
                        Booker = user,
                        TravelId = travelId,
                        Travel = travel,
                    });
            }
            return null;
        }

        public List<TravelBooking> GetUserPastTravels(string userId)
        {
            return _pastTravelRepository.GetAll(selector: x => x,
              predicate: x => x.BookerId == userId).ToList();
        }

        public List<Travel> GetUserPastTravelsObjects(string userId)
        {
            var travelIds = GetUserPastTravels(userId).Select(w => w.TravelId).ToList();

            var travels = _travelService.GetByIdsWithCity(travelIds);

            return travels;
        }

        public bool IsInPastTravels(Guid travelId, string userId)
        {
            return _pastTravelRepository.Get(selector: x => x,
                predicate: x => x.TravelId == travelId && x.BookerId == userId) == null ? false : true;
        }

        public bool RemoveFromPastTravels(Guid travelId, string userId)
        {
            var pastTravelItem = _pastTravelRepository.Get(
                 selector: x => x,
                 predicate: x => x.TravelId == travelId && x.BookerId == userId);

            if (pastTravelItem != null)
            {
                _pastTravelRepository.Delete(pastTravelItem);
                return true;
            }
            return false;
        }
    }
}
