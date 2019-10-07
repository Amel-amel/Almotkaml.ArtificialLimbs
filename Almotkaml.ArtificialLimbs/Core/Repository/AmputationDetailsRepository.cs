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
    public class AmputationDetailsRepository : Repository<AmputationDetails>, IAmputationDetailsRepository
    {
        private ArtificialLimbsDbContext Context { get; }
        internal AmputationDetailsRepository(ArtificialLimbsDbContext context)
            : base(context)
        {
            Context = context;
        }

        public IEnumerable<AmputationDetails> GetAmputationWithDetails()
        => _context.AmputationDetails.Include(x => x.amputationStatues);

        public IEnumerable<AmputationDetails> GetAmputationWithDetails(int id)
          => _context.AmputationDetails
                      .Include(x => x.amputationStatues)
                      .Where(x => x.AmputationStatuesID == id);
        public IEnumerable<AmputationDetails> GetAmputationWithDetails2(int id)
        => _context.AmputationDetails
                .Include(x => x.amputationStatues)
                .Include(x => x.amputationStatues.patient)
                .Where(x => x.AmputationDetailsID == id);

        public AmputationDetails Find(object id)
        {
            return Context.AmputationDetails
                .Include(e => e.amputationStatues)
                .FirstOrDefault(v => v.AmputationStatuesID == (int)id);
        }

        public AmputationDetails GetAmputationDetails(object id)
        {
            return Context.AmputationDetails
                .Include(e => e.amputationStatues)
                .FirstOrDefault(v => v.AmputationDetailsID == (int)id);
        }

    }
}