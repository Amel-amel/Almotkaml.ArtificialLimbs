using Almotkaml.ArtificialLimbs.Core.Domain;
using Almotkaml.ArtificialLimbs.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Almotkaml.ArtificialLimbs.Core.Extensions
{
    public static class AmputationTypesExtensions
    {
        public static IEnumerable<AmputationTypesListItem> ToList(this IEnumerable<AmputationTypes> amputationTypes)
        => amputationTypes.Select(d => new AmputationTypesListItem()
        {
            Name = d.Name,
            AmputationTypesID  = d.AmputationTypeID,
        });

        public static IEnumerable<AmputationTypesGridRow> ToGrid(this IEnumerable<AmputationTypes> amputationTypes)
            => amputationTypes.Select(d => new AmputationTypesGridRow()
            {
                Name = d.Name,
                AmputationTypesID = d.AmputationTypeID,
            });
    }
}