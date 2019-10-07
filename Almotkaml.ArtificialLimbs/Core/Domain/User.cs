using Almotkaml.ArtificialLimbs.Global;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Almotkaml.ArtificialLimbs.Core.Domain
{
    public class User
    {
        //public static INameHolder New()
        //{
        //    return new UserBuilder();
        //}

        internal User() { }

        public int UserId { get; internal set; }
        public string Title { get; internal set; }
        public string UserName { get; internal set; }
        public string Password { get; internal set; }
        public int UserGroupId { get; internal set; }
        public UserGroup UserGroup { get; internal set; }
        //public Category Notify { get; internal set; }
        //public ICollection<Activity> Activities { get; } = new HashSet<Activity>();
        //public ICollection<Notification> Notifications { get; internal set; } = new HashSet<Notification>();

        public static User New(string _name, string _title, string _password, int _groupId)
        {
            Check.NotEmpty(_title, nameof(_title));
            Check.NotNull(_name, nameof(_name));
            Check.NotEmpty(_password, nameof(_password));
            Check.NotNull(_groupId, nameof(_groupId));

            var group = new User()
            {
                Title = _title,
                UserName = _name,
                Password = Hash.MD5(_password),
                UserGroupId = _groupId,
            };

            return group;
        }
        public void Modify(string _title, string _name, string _password, int _groupId)
        {
            Check.NotEmpty(_title, nameof(_title));
            Check.NotNull(_name, nameof(_name));
            Check.NotEmpty(_password, nameof(_password));
            Check.NotNull(_groupId, nameof(_groupId));

            Title = _title;
            UserName = _name;
            Password = Hash.MD5(_password);
            UserGroupId = _groupId;

        }

    }
}