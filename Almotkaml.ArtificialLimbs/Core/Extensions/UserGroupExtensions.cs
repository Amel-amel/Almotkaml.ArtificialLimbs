using Almotkaml.ArtificialLimbs.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Almotkaml.ArtificialLimbs.Core.Extensions
{
    public static class UserGroupExtensions
    {
        public static IEnumerable<UserGroupGridRow> ToGrid(this IEnumerable<Domain.UserGroup> usergroup)
            => usergroup.Select(d => new UserGroupGridRow()
            {
                UserGroupID = d.UserGroupId,
                GroupTitle = d.Name,
                _Permission = d._Permissions,
            });

        public static IEnumerable<UserGroupListItem> ToList(this IEnumerable<Domain.UserGroup> usergroup)
        => usergroup.Select(d => new UserGroupListItem()
        {
            UserGroupID = d.UserGroupId,
            GroupTitle = d.Name,
        });
    }
}