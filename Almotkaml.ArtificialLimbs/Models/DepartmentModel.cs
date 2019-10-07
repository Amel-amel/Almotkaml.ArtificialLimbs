using Almotkaml.ArtificialLimbs.Core.Resources;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Almotkaml.ArtificialLimbs.Models
{
    public class DepartmentModel
    {
        public int DepartmentID { get; set; }

        [Required(ErrorMessageResourceType = typeof(SharedMessages),
        ErrorMessageResourceName = nameof(SharedMessages.IsRequired))]
        [Display(ResourceType = typeof(titles), Name = nameof(titles.Department))]
        public string DepartmentName { get; set; }

        public IEnumerable<DepartmentGridRow> DepartmentGrid { get; set; } = new HashSet<DepartmentGridRow>();
        public IEnumerable<DepartmentListItem> DepartmentList { get; set; } = new HashSet<DepartmentListItem>();

        public bool CanCreate { get; set; }
        public bool CanEdit { get; set; }
        public bool CanDelete { get; set; }
    }

    public class DepartmentListItem
    {
        public int DepartmentID { get; set; }
        public string DepartmentName { get; set; }
    }

    public class DepartmentGridRow
    {
        public int DepartmentID { get; set; }
        public string DepartmentName { get; set; }
    }
}