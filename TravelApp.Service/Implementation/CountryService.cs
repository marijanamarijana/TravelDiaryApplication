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
    public class CountryService : ICountryService
    {
        private readonly IRepository<Country> _repository;
        
        public CountryService(IRepository<Country> repository)
        {
            _repository = repository;
        }

        public Country Add(Country country)
        {
            country.Id = Guid.NewGuid();
            return _repository.Insert(country);
        }

        public Country DeleteById(Guid Id)
        {
            var country = _repository.Get(selector: x => x,
                                               predicate: x => x.Id == Id);
            return _repository.Delete(country);
        }

        public List<Country> GetAll()
        {
            return _repository.GetAll(selector: x => x).ToList();
        }

        public Country? GetById(Guid Id)
        {
            return _repository.Get(selector: x => x,
                                    predicate: x => x.Id == Id);
        }

        public Country Update(Country country)
        {
            return _repository.Update(country);
        }
    }
}
