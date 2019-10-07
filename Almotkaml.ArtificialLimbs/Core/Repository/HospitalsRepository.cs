using Almotkaml.ArtificialLimbs.Core.Domain;
using Almotkaml.ArtificialLimbs.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Almotkaml.ArtificialLimbs.Core.Repository
{
    public class HospitalsRepository : Repository<Hospitals> , IHospitalsRepository
    {
        protected readonly ArtificialLimbsDbContext _context;
        public HospitalsRepository(ArtificialLimbsDbContext context)
            : base(context)
        {
            _context = context;
        }
    }
}