using Almotkaml.ArtificialLimbs.Core.Resources;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Security.AccessControl;
using System.Web;

namespace Almotkaml.ArtificialLimbs.Models
{
    public class BranchModel
    {
        public int BranchId { get;  set; }
        [Required(ErrorMessageResourceType = typeof(SharedMessages),
        ErrorMessageResourceName = nameof(SharedMessages.IsRequired))]
        [Display(ResourceType = typeof(titles), Name = nameof(titles.BranchName))]
        public string BranchName { get;  set; }

        public IEnumerable<BranchGridRow> BranchGrid { get; set; } = new HashSet<BranchGridRow>();
        public IEnumerable<BranchListItem> BranchList { get; set; } = new HashSet<BranchListItem>();
        public bool CanCreate { get; set; }
        public bool CanEdit { get; set; }
        public bool CanDelete { get; set; }
    }

    public class BranchGridRow
    {
        public int BranchId { get;  set; }
        public string Name { get;  set; }
    }

    public class BranchListItem
    {
        public int BranchId { get;  set; }
        public string Name { get;  set; }
    }
}