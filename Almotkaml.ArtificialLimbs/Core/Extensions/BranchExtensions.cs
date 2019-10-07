using Almotkaml.ArtificialLimbs.Core.Domain;
using Almotkaml.ArtificialLimbs.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Almotkaml.ArtificialLimbs.Core.Extensions
{
    public static class BranchExtensions
    {
        public static IEnumerable<BranchListItem> ToList(this IEnumerable<Branch> cities)
           => cities.Select(d => new BranchListItem()
           {
               Name = d.Name,
               BranchId = d.BranchId
           });

        public static IEnumerable<BranchGridRow> ToGrid(this IEnumerable<Branch> cities)
            => cities.Select(d => new BranchGridRow()
            {
                Name = d.Name,
                BranchId = d.BranchId
            });
    }
}