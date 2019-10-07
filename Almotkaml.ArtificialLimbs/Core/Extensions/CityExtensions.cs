using Almotkaml.ArtificialLimbs.Core.Domain;
using Almotkaml.ArtificialLimbs.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Almotkaml.ArtificialLimbs.Core.Extensions
{
    public static class CityExtensions
    {
        public static IEnumerable<CityListItem> ToList(this IEnumerable<City> cities)
            => cities.Select(d => new CityListItem()
            {
                Name = d.Name,
                CityId = d.CityId
            });

        public static IEnumerable<CityGridRow> ToGrid(this IEnumerable<City> cities)
            => cities.Select(d => new CityGridRow()
            {
                CityId = d.CityId,
                CountryId = d.CountryId,
                CountryName = d.Country?.Name,
                Name = d.Name
            });
    }
}