using Almotkaml.ArtificialLimbs.Core.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Almotkaml.ArtificialLimbs.Core.Interfaces
{
    public interface IAmputationDetailsRepository : IRepository<AmputationDetails>
    {
        IEnumerable<AmputationDetails> GetAmputationWithDetails();
        IEnumerable<AmputationDetails> GetAmputationWithDetails(int id);
        IEnumerable<AmputationDetails> GetAmputationWithDetails2(int id);
        AmputationDetails Find(object id);
        AmputationDetails GetAmputationDetails(object id);
    }
}
