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
    public class DailyDetailsRepository : Repository<DailyDetails>, IDailyDetailsRepository
    {
        private ArtificialLimbsDbContext Context { get; }
        internal DailyDetailsRepository(ArtificialLimbsDbContext context)
            : base(context)
        {
            Context = context;
        }

        public IEnumerable<DailyDetails> GetDailyWithDetails()
        => _context.DailyDetails.Include(x => x.dailyStatues);

        public IEnumerable<DailyDetails> GetDailyWithDetails(int id)
          =>  _context.DailyDetails
                      .Include(x => x.dailyStatues)
                      .Include(x => x.dailyStatues.patient)
                      .Where(i => i.DailyDetailsID == id);
        public IEnumerable<DailyDetails> GetDailyWithDetails2(int id)
        => _context.DailyDetails
               .Include(x => x.dailyStatues)
               .Include(x => x.dailyStatues.patient)
               .Where(i => i.DailyStatuesID == id);

        public DailyDetails Find(object id)
        {
            return Context.DailyDetails
                .Include(e => e.dailyStatues)
                .FirstOrDefault(v => v.DailyStatuesID == (int)id);
        }

        public DailyDetails GetDailyDetails(object id)
        {
            return Context.DailyDetails
                .Include(e => e.dailyStatues)
                .FirstOrDefault(v => v.DailyDetailsID == (int)id);
        }
    }

}