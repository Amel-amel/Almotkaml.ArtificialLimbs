using Almotkaml.ArtificialLimbs.Core.Domain;
using Almotkaml.ArtificialLimbs.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Almotkaml.ArtificialLimbs.Core.Extensions
{
    public static class NationalityExtensions
    {
      
            public static IEnumerable<NationalityListItem> ToList(this IEnumerable<Nationality> Country)
               => Country.Select(d => new NationalityListItem()
               {
                   Name = d.Name,
                   NationalityId = d.NationalityId
               });

            public static IEnumerable<NationalityGridRow> ToGrid(this IEnumerable<Nationality> Country)
               => Country.Select(d => new NationalityGridRow()
               {
                   Name = d.Name,
                   NationalityId = d.NationalityId
               });
        
    }
}