using Almotkaml.ArtificialLimbs.Core.Domain;
using Almotkaml.ArtificialLimbs.Core.Interfaces;
using System.Collections.Generic;
using System.Linq;

namespace Almotkaml.ArtificialLimbs.Core.Repository
{
    public class CityRepository : Repository<City>, ICityRepository
    {
         protected readonly ArtificialLimbsDbContext _Context;
        public CityRepository(ArtificialLimbsDbContext context)
            : base(context)
        {
            _Context = context;
        }


     

        //public IEnumerable<City> GetCityWithCountry(int countryId)
        //{
        //    return _context.Cities
        //        .Include(d => d.Country)
        //        .Where(d => d.CountryId == countryId);
        //}

        //public IEnumerable<City> GetCityWithCountry()
        //{
        //    return _context.Cities
        //        .Include(d => d.Country);
        //}

        public bool CityExisted(string name, int countryId) => _context.Cities
            .Any(e => e.Name == name && e.CountryId == countryId);

        public bool CityExisted(string name, int countryId, int idToExcept) => _context.Cities
            .Any(e => e.Name == name && e.CityId != idToExcept && e.CountryId == countryId);

    }
}