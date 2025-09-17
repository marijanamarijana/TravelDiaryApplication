using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TravelApp.Domain.DomainModels;
using TravelApp.Repository;
using TravelApp.Service.Interface;

namespace TravelApp.Service.Implementation
{
    public class CityService : ICityService
    {
        private readonly IRepository<City> _repository;

        public CityService(IRepository<City> cityRepository)
        {
            _repository = cityRepository;
        }
        public City Add(City city)
        {
            city.Id = Guid.NewGuid();
            return _repository.Insert(city);
        }

        public City DeleteById(Guid Id)
        {
            var city = _repository.Get(selector: x => x,
                                    predicate: x => x.Id == Id);
            return _repository.Delete(city);
        }

        public List<City> GetAll()
        {
            return _repository.GetAll(selector: x => x, 
                include: x => x.Include(y => y.Country)).ToList();
        }

        public City? GetById(Guid Id)
        {
            return _repository.Get(selector: x => x,
                                    predicate: x => x.Id == Id);
        }

        public City Update(City city)
        {
           return _repository.Update(city);
        }
    }
}
