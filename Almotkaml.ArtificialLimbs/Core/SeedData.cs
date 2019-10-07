using Almotkaml.ArtificialLimbs.Global;
using Almotkaml.ArtificialLimbs.Global.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Script.Serialization;

namespace Almotkaml.ArtificialLimbs.Core
{
    internal static class SeedData
    {
        private static ArtificialLimbsDbContext Context { get; set; }
        public static void Load(ArtificialLimbsDbContext context)
        {
            Context = context;
            SeedUserWithGroups();
        }

        private static void SeedUserWithGroups()
        {
            if (Context.Users.Any())
                return;

            var Per = new Permission();
            string stringPermission = new JavaScriptSerializer().Serialize(Per).Replace("false", "true");

            var userGroup = ObjectCreator.Create<Domain.UserGroup>(typeof(Domain.UserGroup));
            userGroup.SetValue(g => g.Name, "Administrator");
            userGroup.SetValue(g => g._Permissions, stringPermission);

            Context.UserGroups.Add(userGroup);

            var stringNotifications = new Permission().ToSerializedObject().Replace("false", "true");

            var fullNotifications = stringNotifications.ToDeserializedObject<Permission>();

            var user = ObjectCreator.Create<Domain.User>(typeof(Domain.User));
            user.SetValue(g => g.UserGroup, userGroup);
            user.SetValue(g => g.Password, Hash.MD5("!QA2ws3ed"));
            user.SetValue(g => g.Title, "Almotkaml");
            user.SetValue(g => g.UserName, "Admin");

            Context.Users.Add(user);
            Context.SaveChanges();
        }
    }
}