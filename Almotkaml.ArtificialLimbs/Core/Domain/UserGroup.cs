using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Almotkaml.ArtificialLimbs.Core.Domain
{
    public class UserGroup
    {
        public int UserGroupId { get; private set; }
        public string Name { get; private set; }
        public string _Permissions { get; set; }
        //public ICollection< Permission>  Permissions { get; private set; }

        private UserGroup()
        {

        }

        public static UserGroup Existed(int userGroupId, string name, string permissions)
        {
            Check.NotEmpty(name, nameof(name));
            Check.NotNull(permissions, nameof(permissions));
            Check.MoreThanZero(userGroupId, nameof(userGroupId));

            return new UserGroup()
            {
                UserGroupId = userGroupId,
                Name = name,
                _Permissions = permissions,
            };
        }

        public static UserGroup New(string name, string permissions)
        {
            Check.NotEmpty(name, nameof(name));
            Check.NotNull(permissions, nameof(permissions));

            var group = new UserGroup()
            {
                Name = name,
                _Permissions = permissions,
            };

            return group;
        }
        public void Modify(string name, string permissions)
        {
            Check.NotEmpty(name, nameof(name));
            Check.NotNull(permissions, nameof(permissions));

            Name = name;
            _Permissions = permissions;

        }
        public ICollection<User> Users { get; } = new HashSet<User>();
    }
}