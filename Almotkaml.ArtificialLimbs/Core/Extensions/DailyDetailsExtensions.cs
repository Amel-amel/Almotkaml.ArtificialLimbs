using Almotkaml.ArtificialLimbs.Core.Domain;
using Almotkaml.ArtificialLimbs.Global;
using Almotkaml.ArtificialLimbs.Global.Extensions;
using Almotkaml.ArtificialLimbs.Models;
using Almotkaml.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Almotkaml.ArtificialLimbs.Core.Extensions
{
    public static class DailyDetailsExtensions
    {

        public static IEnumerable<DailyDetailsGridRow> ToGrid(this IEnumerable<DailyDetails> dailyDetails)
          => dailyDetails.Select(d => new DailyDetailsGridRow()
          {
            DailyDetailsID          =d.DailyDetailsID,
            DailyStatuesID          = d.DailyStatuesID,
            //AmputationPart          = d.AmputationPart,
            MeasurementModel        = d.MeasurementModel,
            Note              = d.Note,
            MeasurementNumberModel  = d.Part,
            PartMeasure             = d.PartMeasure,
            ShoeNO                  = d.shoesNo,
              
          });

       

    }
}