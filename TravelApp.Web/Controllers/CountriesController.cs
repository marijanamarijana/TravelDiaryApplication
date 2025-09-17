using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TravelApp.Domain.DomainModels;
using TravelApp.Repository;
using TravelApp.Service.Implementation;
using TravelApp.Service.Interface;

namespace TravelApp.Web.Controllers
{
    public class CountriesController : Controller
    {
        private readonly ICountryService _service;
        private readonly IApiService _apiService;

        public CountriesController(ICountryService service, IApiService apiService)
        {
            _service = service;
            _apiService = apiService;
        }

        // GET: Countries
        public async Task<IActionResult> Index()
        {
            return View(_service.GetAll());
        }

        // GET: Countries/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var country = _service.GetById(id.Value);
            if (country == null)
            {
                return NotFound();
            }

            return View(country);
        }

        // GET: Countries/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Countries/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Name,ISO2Code,Id")] Country country)
        {
            if (ModelState.IsValid)
            {
                _service.Add(country);
                return RedirectToAction(nameof(Index));
            }
            return View(country);
        }

        // GET: Countries/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var country = _service.GetById(id.Value);
            if (country == null)
            {
                return NotFound();
            }
            return View(country);
        }

        // POST: Countries/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind("Name,ISO2Code,Id")] Country country)
        {
            if (id != country.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _service.Update(country);
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CountryExists(country.Id))
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
            return View(country);
        }

        // GET: Countries/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var country = _service.GetById(id.Value);
            if (country == null)
            {
                return NotFound();
            }
            return View(country);
        }

        // POST: Countries/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            _service.DeleteById(id);
            return RedirectToAction(nameof(Index));
        }

        private bool CountryExists(Guid id)
        {
            return _service.GetById(id) == null ? false : true;
        }

        [HttpGet("Countries/Info/{ISO2Code}")]
        public async Task<IActionResult> Info(string ISO2Code)
        {
            if (string.IsNullOrEmpty(ISO2Code))
                return NotFound();

           var country = await _apiService.GetCountryInfoAsync(ISO2Code);

            return View(country);
        }


        [HttpGet("Countries/Flag/{ISO2Code}")]
        public async Task<IActionResult> CountryFlag(string ISO2Code)
        {
            var countryFlag = await _apiService.GetCountryFlagAsync(ISO2Code);

            //if (countryFlag == null)
            //    return NotFound();

            return View(countryFlag);
        }
    }
}
