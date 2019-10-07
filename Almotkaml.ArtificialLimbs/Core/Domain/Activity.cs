using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Almotkaml.ArtificialLimbs.Core.Domain
{
    public class Activity
    {
        public static Activity New(int userId, string activityType)
        {
            Check.MoreThanZero(userId, nameof(userId));
            Check.NotEmpty(activityType, nameof(activityType));

            var activtiy = new Activity()
            {
                UserId = userId,
                Type = activityType
            };

            // not completed ...
            return activtiy;
        }

        public static Activity New(User user, string activityType)
        {
            Check.NotNull(user, nameof(user));
            Check.NotEmpty(activityType, nameof(activityType));

            var activtiy = new Activity()
            {
                FiredBy_User = user,
                Type = activityType
            };

            return activtiy;
        }

        private Activity() { }

        // ReSharper disable once UnusedAutoPropertyAccessor.Local
        public long ActivityId { get; set; }
        public DateTime DateTime { get; set; } = DateTime.Now;
        public int UserId { get; set; }
        public User FiredBy_User { get; set; }
        public string Type { get; private set; }
        // public ICollection<Notification> Notifications { get; } = new HashSet<Notification>();
    }
}