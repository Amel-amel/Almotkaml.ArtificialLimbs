using Almotkaml.ArtificialLimbs.Core.Domain;
using Almotkaml.ArtificialLimbs.Core.Interfaces;
using Almotkaml.ArtificialLimbs.Models;
using System;
using System.Data.Entity;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Almotkaml.ArtificialLimbs.Core.Repository
{
    public class ActivityRepository : Repository<Activity>, IActivityRepository
    {
        private ArtificialLimbsDbContext Context { get; }
        internal ActivityRepository(ArtificialLimbsDbContext context)
            : base(context)
        {
            Context = context;
        }


        public IEnumerable<Activity> GetUserActivities(DateTime fromDate, DateTime toDate, int userId)
        {
            var userActivity = Context.Activities
                .Include(i => i.FiredBy_User)
                .Where(u => u.DateTime >= fromDate && u.DateTime <= toDate);
            if (userId > 0)
                userActivity = userActivity.Where(u => u.UserId == userId);

            return userActivity;

        }

        public IEnumerable<Activity> Search(ActivityModel model)
        {
            IEnumerable<Activity> Activities = _context.Activities
                                                                .Include(a => a.FiredBy_User)
                                                                .ToList();

            if (model.UserId > 0)
                Activities = Activities.Where(e => e.UserId == model.UserId);

            if (model.DateFrom != null && model.DateTo != null)
                Activities = Activities.Where(e => e.DateTime > Convert.ToDateTime(model.DateFrom) & e.DateTime < Convert.ToDateTime(model.DateTo));

            return Activities;
        }
    }

}