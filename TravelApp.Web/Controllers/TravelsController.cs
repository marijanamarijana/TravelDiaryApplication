using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Diagnostics.Metrics;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using TravelApp.Domain.DomainModels;
using TravelApp.Repository;
using TravelApp.Service.Implementation;
using TravelApp.Service.Interface;

namespace TravelApp.Web.Controllers
{
    public class TravelsController : Controller
    {
        private readonly ITravelService _travelService;
        private readonly ICityService _cityService;
        private readonly IWishlistService _wishlistService;
        private readonly IPastTravelsService _pastTravelsService;

        public TravelsController(ITravelService travelService, ICityService cityService, IWishlistService wishlistService, IPastTravelsService pastTravelsService)
        {
            _travelService = travelService;
            _cityService = cityService;
            _wishlistService = wishlistService;
            _pastTravelsService = pastTravelsService;
        }

        // GET: Travels
        public async Task<IActionResult> Index()
        {
            var travels = _travelService.GetAll();
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            var wishlistTravelIds = string.IsNullOrEmpty(userId)
                ? new List<Guid>()
                : _wishlistService.GetUserWishlist(userId).Select(w => w.TravelId).ToList();

            var pastTravelsIds = string.IsNullOrEmpty(userId)
                ? new List<Guid>()
                : _pastTravelsService.GetUserPastTravels(userId).Select(w => w.TravelId).ToList();

            ViewBag.WishlistTravelIds = wishlistTravelIds;
            ViewBag.PastTravelsIds = pastTravelsIds;

            return View(travels);
        }

        // GET: Travels/Details/5
         public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var travel = _travelService.GetById(id.Value);
            if (travel == null)
            {
                return NotFound();
            }
            var cities = _cityService.GetAll();
            ViewBag.Cities = new MultiSelectList(cities, "Id", "Name", travel.Cities?.Select(c => c.Id));
            return View(travel);
        }

        // GET: Travels/Create
        public IActionResult Create()
        {
            var cities = _cityService.GetAll();
            ViewBag.Cities = new MultiSelectList(cities, "Id", "Name");
            return View();
        }

        // POST: Travels/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Title,Description,StartDate,EndDate,Price,VisaNeeded,Id")] Travel travel, List<Guid> selectedCities)
        {
            if (ModelState.IsValid)
            {
                if (selectedCities != null && selectedCities.Any())
                {
                    var cities0 = _cityService.GetAll().Where(c => selectedCities.Contains(c.Id)).ToList();
                    travel.Cities = cities0;
                }
                _travelService.Add(travel);
                return RedirectToAction(nameof(Index));
            }
            var cities = _cityService.GetAll();
            ViewBag.Cities = new MultiSelectList(cities, "Id", "Name", selectedCities);
            return View(travel);
        }

        // GET: Travels/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null) return NotFound();

            var travel = _travelService.GetById(id.Value);
            if (travel == null) return NotFound();

            var cities = _cityService.GetAll();
            var selected = travel.Cities?.Select(c => c.Id).ToList();

            ViewBag.Cities = new MultiSelectList(cities, "Id", "Name", selected);
            return View(travel);
        }

        // POST: Travels/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind("Title,Description,StartDate,EndDate,Price,VisaNeeded,Id")] Travel travel, List<Guid> selectedCities)
        {
            if (id != travel.Id) return NotFound();

            if (ModelState.IsValid)
            {
                try
                {
                    _travelService.Update(travel, selectedCities);
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (_travelService.GetById(travel.Id) == null)
                        return NotFound();
                    throw;
                }
                return RedirectToAction(nameof(Index));
            }

            var cities = _cityService.GetAll();
            ViewBag.Cities = new MultiSelectList(cities, "Id", "Name", selectedCities);
            return View(travel);
        }
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var travel = _travelService.GetById(id.Value);
            if (travel == null)
            {
                return NotFound();
            }

            var cities = _cityService.GetAll();
            ViewBag.Cities = new MultiSelectList(cities, "Id", "Name", travel.Cities?.Select(c => c.Id));

            return View(travel);
        }

        // POST: Travels/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            _travelService.DeleteById(id);
            return RedirectToAction(nameof(Index));
        }
        private bool TravelExists(Guid id)
        {
            return _travelService.GetById(id) == null ? false : true;
        }
    }
}
