using Almotkaml.ArtificialLimbs.Core.Resources;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Almotkaml.ArtificialLimbs.Models
{
    public class EmployeeModel
    {
        public int EmployeeID { get; set; }

        //[Required(ErrorMessageResourceType = typeof(SharedMessages),
        //ErrorMessageResourceName = nameof(SharedMessages.IsRequired))]
        //[Display(ResourceType = typeof(titles), Name = nameof(titles.EmployeeName))]
        public int HREmpID { get; set; }

        [Required(ErrorMessageResourceType = typeof(SharedMessages),
        ErrorMessageResourceName = nameof(SharedMessages.IsRequired))]
        [Display(ResourceType = typeof(titles), Name = nameof(titles.EmployeeName))]
        public string EmployeeName { get; set; }

        [Required(ErrorMessageResourceType = typeof(SharedMessages),
        ErrorMessageResourceName = nameof(SharedMessages.IsRequired))]
        [Display(ResourceType = typeof(titles), Name = nameof(titles.Department))]
        public int DepartmentID { get; set; }

        public IEnumerable<EmployeeGridRow> EmployeeGrid { get; set; } = new HashSet<EmployeeGridRow>();
        public IEnumerable<Core.Linq.Employee> HREmployeeGrid { get; set; } = new HashSet<Core.Linq.Employee>();
        public IEnumerable<EmployeeListItem> EmployeeList { get; set; } = new HashSet<EmployeeListItem>();
        public IEnumerable<DepartmentListItem> DepartmentList { get; set; } = new HashSet<DepartmentListItem>();

        public bool CanCreate { get; set; }
        public bool CanEdit { get; set; }
        public bool CanDelete { get; set; }
    }

    public class EmployeeListItem
    {
        public int EmployeeID { get; set; }
        public string EmployeeName { get; set; }
    }

    public class EmployeeGridRow
    {
        public int EmployeeID { get; set; }
        public string EmployeeName { get; set; }
        public int DepartmentID { get; set; }
        public string DepartmentName { get; set; }
    }
}