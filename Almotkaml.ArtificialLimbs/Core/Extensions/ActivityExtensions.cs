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
    public static class ActivityExtensions
    {

        public static IEnumerable<UserActivityGridrow> ToGrid(this IEnumerable<Activity> activities)
          => activities.Select(d => new UserActivityGridrow()
          {
              DateTime = d.DateTime.ToString(),
              ActivityId = d.ActivityId,
              Type = typeof(Permission).GetAttribute<PhraseAttribute>(d.Type)?.Display,
              userID = d.UserId,
              Title = d.FiredBy_User?.UserName,
          });

    }
}