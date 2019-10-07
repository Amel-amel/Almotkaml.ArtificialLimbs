using Almotkaml.ArtificialLimbs.Core.Domain;
using Almotkaml.ArtificialLimbs.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Almotkaml.ArtificialLimbs.Core.Repository
{
    public class DepartmentRepository : Repository<Department>, IDepartmentRepository 
    {

        public DepartmentRepository(ArtificialLimbsDbContext context)
            : base(context)
        {

        }
    }
}