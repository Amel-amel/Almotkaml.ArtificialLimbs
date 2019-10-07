using Almotkaml.ArtificialLimbs.Core.Domain;
using Almotkaml.ArtificialLimbs.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Almotkaml.ArtificialLimbs.Core.Extensions
{
    public static class HospitalsExtensions
    {
        public static IEnumerable<HospitalsListItem> ToList(this IEnumerable<Hospitals> hospitals)
         => hospitals.Select(d => new HospitalsListItem()
         {
             Name = d.Name,
             HospitalsId = d.HospitalsId,
         });

        public static IEnumerable<HospitalsGridRow> ToGrid(this IEnumerable<Hospitals> hospital)
            => hospital.Select(d => new HospitalsGridRow()
            {
                Name = d.Name,
                HospitalsId = d.HospitalsId,
            });
    }
}