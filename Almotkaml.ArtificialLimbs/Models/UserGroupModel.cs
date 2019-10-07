using Almotkaml.ArtificialLimbs.Global;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Almotkaml.ArtificialLimbs.Models
{
    public class UserGroupModel
    {
        public int UserGroupID { get; set; }

        public bool CanCreate { get; set; }
        public bool CanEdit { get; set; }
        public bool CanDelete { get; set; }

        [Required(ErrorMessage = "يجب إدخال اسم المجموعة قبل المتابعة .")]
        [Display(Name = "اسم المجموعة")]
        public string GroupTitle { get; set; }

        public Permission Permission { get; set; }

        public IEnumerable<UserGroupGridRow> UserGroupGrid { get; set; } = new HashSet<UserGroupGridRow>();
        public IEnumerable<UserGroupListItem> UserGroupList { get; set; } = new HashSet<UserGroupListItem>();
    }

    public class UserGroupGridRow
    {
        public int UserGroupID { get; set; }
        public string GroupTitle { get; set; }
        public string _Permission { get; set; }
    }

    public class UserGroupListItem
    {
        public int UserGroupID { get; set; }
        public string GroupTitle { get; set; }
    }

}