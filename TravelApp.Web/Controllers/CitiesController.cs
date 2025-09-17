using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TravelApp.Domain.DomainModels;
using TravelApp.Service.Implementation;
using TravelApp.Service.Interface;

namespace TravelApp.Web.Controllers
{
    public class CitiesController : Controller
    {
        private readonly ICityService _cityService;
        private readonly ICountryService _countryService;

        public CitiesController(ICityService service, ICountryService countryService)
        {
            _cityService = service;
            _countryService = countryService;
        }

        // GET: Cities
        public async Task<IActionResult> Index()
        {
            return View(_cityService.GetAll());
        }

        // GET: Cities/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var city = _cityService.GetById(id.Value);
            if (city == null)
            {
                return NotFound();
            }
            var countries = _countryService.GetAll();
            ViewBag.CountryId = new SelectList(countries, "Id", "Name", city.CountryId);
            return View(city);
        }

        // GET: Cities/Create
        public IActionResult Create()
        {
            var countries = _countryService.GetAll();
            ViewBag.CountryId = new SelectList(countries, "Id", "Name");
            return View();
        }

        // POST: Cities/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Name,CountryId,Id")] City city)
        {
            if (ModelState.IsValid)
            {
                _cityService.Add(city);
                return RedirectToAction(nameof(Index));
            }
            var countries = _countryService.GetAll();
            ViewBag.CountryId = new SelectList(countries, "Id", "Name", city.CountryId);
            return View(city);
        }

        // GET: Cities/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var city = _cityService.GetById(id.Value);
            if (city == null)
            {
                return NotFound();
            }
            var countries = _countryService.GetAll();
            ViewBag.CountryId = new SelectList(countries, "Id", "Name", city.CountryId);
            return View(city);
        }

        // POST: Cities/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind("Name,CountryId,Id")] City city)
        {
            if (id != city.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _cityService.Update(city);
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CityExists(city.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            var countries = _countryService.GetAll();
            ViewBag.CountryId = new SelectList(countries, "Id", "Name", city.CountryId);
            return View(city);
        }

        // GET: Cities/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var city = _cityService.GetById(id.Value);
            if (city == null)
            {
                return NotFound();
            }
            var countries = _countryService.GetAll();
            ViewBag.CountryId = new SelectList(countries, "Id", "Name", city.CountryId);
            return View(city);
        }

        // POST: Cities/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            _cityService.DeleteById(id);
            return RedirectToAction(nameof(Index));
        }

        private bool CityExists(Guid id)
        {
            return _cityService.GetById(id) == null ? false : true;
        }
    }
}
