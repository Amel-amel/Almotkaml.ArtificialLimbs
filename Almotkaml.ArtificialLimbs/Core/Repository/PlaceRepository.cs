using Almotkaml.ArtificialLimbs.Core.Domain;
using Almotkaml.ArtificialLimbs.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Almotkaml.ArtificialLimbs.Core.Repository
{
    public class PlaceRepository : Repository<Place>, IPlaceRepository
    {

        public PlaceRepository(ArtificialLimbsDbContext context)
            : base(context)
        {

        }
    }
}