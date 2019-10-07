using Almotkaml.ArtificialLimbs.Core.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Almotkaml.ArtificialLimbs.Core.Interfaces
{
    public interface IDailyDetailsRepository : IRepository<DailyDetails>
    {
        IEnumerable<DailyDetails> GetDailyWithDetails();
        IEnumerable<DailyDetails> GetDailyWithDetails(int id);
        IEnumerable<DailyDetails> GetDailyWithDetails2(int id);
        DailyDetails Find(object id);
        DailyDetails GetDailyDetails(object id);

    }
}
