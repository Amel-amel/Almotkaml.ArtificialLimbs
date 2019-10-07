using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using Almotkaml.ArtificialLimbs.Core.Resources;

namespace Almotkaml.ArtificialLimbs.Models
{
    public class UserModel
    {
        public bool CanCreate { get; set; }
        public bool CanEdit { get; set; }
        public bool CanDelete { get; set; }

        public int UserID { get; set; }
        [Required(ErrorMessageResourceType = typeof(Resources.SharedMessages),
            ErrorMessageResourceName = nameof(Resources.SharedMessages.IsRequired))]
        [Display(ResourceType = typeof(Resources.SharedTitles),
            Name = nameof(Resources.SharedTitles.UserTitle))]
        public string UserTitle { get; set; }

        [Required(ErrorMessageResourceType = typeof(Resources.SharedMessages),
            ErrorMessageResourceName = nameof(Resources.SharedMessages.IsRequired))]
        [Display(ResourceType = typeof(Resources.SharedTitles),
            Name = nameof(Resources.SharedTitles.UserName))]
        public string UserName { get; set; }

        [Required(ErrorMessageResourceType = typeof(Resources.SharedMessages),
            ErrorMessageResourceName = nameof(Resources.SharedMessages.IsRequired))]
        [Display(ResourceType = typeof(Resources.SharedTitles),
             Name = nameof(Resources.SharedTitles.Password))]
        public string Password { get; set; }

        [Required(ErrorMessageResourceType = typeof(Resources.SharedMessages),
            ErrorMessageResourceName = nameof(Resources.SharedMessages.IsRequired))]
        [Display(ResourceType = typeof(Resources.SharedTitles),
             Name = nameof(Resources.SharedTitles.ConfirmPassword))]
        public string ConfirmPassword { get; set; }

      [Required(ErrorMessageResourceType = typeof(Resources.SharedMessages),
            ErrorMessageResourceName = nameof(Resources.SharedMessages.IsRequired))]
        [Range(1, double.MaxValue, ErrorMessageResourceType = typeof(Resources.SharedMessages),
            ErrorMessageResourceName = nameof(Resources.SharedMessages.IsRequired))]
        [Display(ResourceType = typeof(Core.Resources.SharedTitles), Name = nameof(Core.Resources.SharedTitles.Group))]
        public int UserGroupId { get; set; }
        public IEnumerable<UserGroupListItem> UserGroupList { get; set; } = new HashSet<UserGroupListItem>();
        public IEnumerable<UserGridRow> UserGrid { get; set; } = new HashSet<UserGridRow>();
        public IEnumerable<UserListItem> UserList { get; set; } = new HashSet<UserListItem>();
        public bool Validate(UserModel model)
        {
            if (Password != ConfirmPassword)
                return false;
            return true;
        }

    }

    public class UserGridRow
    {
        public int UserID { get; set; }
        public string UserTitle { get; set; }
        public string UserName { get; set; }
        public int GroupID { get; set; }
        public string GroupName { get; set; }
    }

    public class UserListItem
    {
        public int UserID { get; set; }
        public string UserTitle { get; set; }
    }


}