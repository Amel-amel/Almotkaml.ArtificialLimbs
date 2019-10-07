using Almotkaml.ArtificialLimbs.Core.Domain;
using Almotkaml.ArtificialLimbs.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Almotkaml.ArtificialLimbs.Core.Extensions
{
    public static class PlaceExtensions
    {
       
            public static IEnumerable<PlaceListItem> ToList(this IEnumerable<Place> Country)
               => Country.Select(d => new PlaceListItem()
               {
                   Name = d.Name,
                   PlaceId = d.PlaceId
               });

            public static IEnumerable<PlaceGridRow> ToGrid(this IEnumerable<Place> Country)
               => Country.Select(d => new PlaceGridRow()
               {
                   Name = d.Name,
                   PlaceId = d.PlaceId,
                   BranchName = d.Branch?.Name,
                   BranchId = d.BranchId
               });
        
    }
}