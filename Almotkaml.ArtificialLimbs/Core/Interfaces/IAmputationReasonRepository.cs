using Almotkaml.ArtificialLimbs.Core.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Almotkaml.ArtificialLimbs.Core.Interfaces
{
    public interface IAmputationReasonRepository : IRepository<AmputationReason>
    {
        bool AmputationReasonExist(string _reason);
        int Find(string _reason);
        int AddNew(string _reason);
    }
}
