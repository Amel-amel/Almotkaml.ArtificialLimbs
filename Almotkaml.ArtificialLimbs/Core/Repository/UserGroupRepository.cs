using Almotkaml.ArtificialLimbs.Core.Domain;
using Almotkaml.ArtificialLimbs.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Almotkaml.ArtificialLimbs.Core.Repository
{
    public class UserGroupRepository : Repository<UserGroup>, IUserGroupRepository
    {
        protected readonly ArtificialLimbsDbContext _Context;
        private readonly UnitOfWork _unitOfWork;

        public UserGroupRepository(ArtificialLimbsDbContext context)
            : base(context)
        {
            _Context = context;
            _unitOfWork = new UnitOfWork(_Context);
        }

        public bool NameExisted(string GroupName)
        {
            return _context.UserGroups.Any(d => d.Name.ToString() == GroupName);
        }
    }
}