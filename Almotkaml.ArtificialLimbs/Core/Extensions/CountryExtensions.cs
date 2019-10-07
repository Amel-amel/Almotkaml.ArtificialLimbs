using Almotkaml.ArtificialLimbs.Core.Domain;
using Almotkaml.ArtificialLimbs.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Almotkaml.ArtificialLimbs.Core.Extensions
{
    public static class CountryExtensions
    {
        public static IEnumerable<CountryListItem> ToList(this IEnumerable<Country> Country)
           => Country.Select(d => new CountryListItem()
           {
               Name = d.Name,
               CountryId = d.CountryId
           });

        public static IEnumerable<CountryGridRow> ToGrid(this IEnumerable<Country> Country)
           => Country.Select(d => new CountryGridRow()
           {
               Name = d.Name,
               CountryId = d.CountryId
           });
    }
}