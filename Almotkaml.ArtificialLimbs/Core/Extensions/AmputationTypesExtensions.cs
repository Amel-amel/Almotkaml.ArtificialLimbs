using Almotkaml.ArtificialLimbs.Core.Domain;
using Almotkaml.ArtificialLimbs.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Almotkaml.ArtificialLimbs.Core.Extensions
{
    public static class AmputationReasonExtensions
    {
        public static IEnumerable<AmputationReasonList> ToList(this IEnumerable<AmputationReason> amputationTypes)
        => amputationTypes.Select(d => new AmputationReasonList()
        {
            AmputationReasonID=d.AmputationReasonID,
            Reason =d.Reason ,
        });

    }
}