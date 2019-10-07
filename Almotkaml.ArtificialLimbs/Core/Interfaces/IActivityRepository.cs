using Almotkaml.ArtificialLimbs.Core.Domain;
using Almotkaml.ArtificialLimbs.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Almotkaml.ArtificialLimbs.Core.Interfaces
{
    public interface IActivityRepository : IRepository<Activity>
    {
       IEnumerable<Activity> GetUserActivities(DateTime fromDate, DateTime toDate, int userId);
       IEnumerable<Activity> Search(ActivityModel model);
    }
}
