using Almotkaml.ArtificialLimbs.Core.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Almotkaml.ArtificialLimbs.Core.Interfaces
{
    public interface ICityRepository : IRepository<City>
    {
        //IEnumerable<City> GetCityWithCountry(int CountryId);
        //IEnumerable<City> GetCityWithCountry();
        bool CityExisted(string name, int CountryId);
        bool CityExisted(string name, int CountryId, int idToExcept);
    }
}
