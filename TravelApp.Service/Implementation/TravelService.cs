using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Identity.Client;
using System;
using System.Collections.Generic;
using System.Diagnostics.Metrics;
using System.Linq;
using System.Security.Claims;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using TravelApp.Domain.DomainModels;
using TravelApp.Domain.Identity;
using TravelApp.Repository;
using TravelApp.Service.Interface;

namespace TravelApp.Service.Implementation
{
    public class TravelService : ITravelService
    {
        private readonly IRepository<Travel> _travelRepository;
        private readonly ICityService _cityService;

        public TravelService(IRepository<Travel> repository, ICityService cityService)
        {
            _travelRepository = repository;
            _cityService = cityService;
        }
        public Travel Add(Travel travel)
        {
            travel.Id = Guid.NewGuid();
            return _travelRepository.Insert(travel);
        }

        public Travel DeleteById(Guid Id)
        {
            var travel = _travelRepository.Get(selector: x => x,
                                              predicate: x => x.Id == Id);
            return _travelRepository.Delete(travel);
        }

        public List<Travel> GetAll()
        {
            return _travelRepository.GetAll(selector: x => x,
                 include: x => x.Include(y => y.Cities)).ToList();
        }

        public Travel? GetById(Guid Id) 
        {
            return _travelRepository.Get(selector: x => x,
                                    predicate: x => x.Id == Id,
                                    include: x => x.Include(t => t.Cities));
        }

        public Travel Update(Travel travel, List<Guid> cityIds)
        {
            var existing = GetById(travel.Id);
            //    _travelRepository.Get(
            //    selector: x => x,
            //    predicate: x => x.Id == travel.Id,
            //    include: x => x.Include(t => t.Cities)
            //);

            if (existing == null) return null;

            existing.Title = travel.Title;
            existing.Description = travel.Description;
            existing.StartDate = travel.StartDate;
            existing.EndDate = travel.EndDate;
            existing.Price = travel.Price;
            existing.VisaNeeded = travel.VisaNeeded;

            if (cityIds != null && cityIds.Any())
            {
                existing.Cities.Clear();

                var trackedCities = cityIds
                    .Select(id => _cityService.GetById(id)) 
                    .Where(c => c != null)
                    .ToList();

                foreach (var city in trackedCities)
                {
                    existing.Cities.Add(city); 
                }
            }

            return _travelRepository.Update(existing);
        }

        public List<Travel> GetByIdsWithCity(List<Guid> travelIds)
        {
            return _travelRepository.GetAll(selector: x => x,
                                            predicate: x => travelIds.Contains(x.Id),
                                            include: x => x.Include(t => t.Cities)).ToList();
        }
    }
}