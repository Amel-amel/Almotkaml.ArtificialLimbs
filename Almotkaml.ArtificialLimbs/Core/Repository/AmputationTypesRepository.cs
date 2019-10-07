using Almotkaml.ArtificialLimbs.Core.Domain;
using Almotkaml.ArtificialLimbs.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Almotkaml.ArtificialLimbs.Core.Repository
{
    public class AmputationTypesRepository : Repository<AmputationTypes>, IAmputationTypesRepository
    {
        protected readonly ArtificialLimbsDbContext _Context;
        public AmputationTypesRepository(ArtificialLimbsDbContext context)
            : base(context)
        {
            _Context = context;
        }
    }
}