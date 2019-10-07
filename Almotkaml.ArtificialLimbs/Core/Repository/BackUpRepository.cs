using Almotkaml.ArtificialLimbs.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Almotkaml.ArtificialLimbs.Core.Repository
{
    public class BackUpRepository : Repository<DBNull> , IBackUpRepository
    {
        protected readonly ArtificialLimbsDbContext _Context;
        private readonly UnitOfWork _UnitOfWork;
        public BackUpRepository(ArtificialLimbsDbContext context)
            : base(context)
        {
            _Context = context;
        }

        public bool BackUp(string path)
        {
            return _UnitOfWork.BackUp(path);
        }
    }
}