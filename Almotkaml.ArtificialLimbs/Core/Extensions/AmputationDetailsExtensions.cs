using Almotkaml.ArtificialLimbs.Core.Domain;
using Almotkaml.ArtificialLimbs.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Almotkaml.ArtificialLimbs.Core.Extensions
{
    public static class AmputationDetailsExtensions
    {
        public static IEnumerable<AmputationDetailsGridRow> ToGrid(this IEnumerable<AmputationDetails> amputationDetails)
         => amputationDetails.Select(d => new AmputationDetailsGridRow()
         {
             AmputationDetailsID    = d.AmputationDetailsID,
             AmputationStatuesID    =d.AmputationStatuesID,
             AmputationPart         = d.amputationStatues?.AmputationPart,
             MeasurementModel       = d.MeasurementModel,
             Note                   = d.Note,
             MeasurementNumberModel = d.Part,
             PartMeasure            = d.PartMeasure,
             ShoeNO                 = d.shoesNo,
             
         });
    }
}