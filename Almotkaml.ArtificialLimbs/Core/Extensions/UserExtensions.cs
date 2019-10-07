using Almotkaml.ArtificialLimbs.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Almotkaml.ArtificialLimbs.Core.Extensions
{
    public static class UserExtensions
    {
        public static IEnumerable<UserGridRow> ToGrid(this IEnumerable<Domain.User> usergroup)
            => usergroup.Select(d => new UserGridRow()
            {
                UserID = d.UserId,
                UserTitle = d.Title,
                UserName = d.UserName,
                GroupID = d.UserGroupId,
                GroupName = d.UserGroup?.Name,
            });


        public static IEnumerable<UserListItem> ToList(this IEnumerable<Domain.User> users)
        => users.Select(u => new UserListItem()
        {
            UserID = u.UserId,
            UserTitle = u.UserName,
        });
    }
}