using Almotkaml.ArtificialLimbs.Core.Enum;
using Almotkaml.ArtificialLimbs.Core.Resources;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Almotkaml.ArtificialLimbs.Models
{
    public class DeviceModel
    {
        public int DeviceID { get; set; }

        [Required(ErrorMessageResourceType = typeof(SharedMessages),
        ErrorMessageResourceName = nameof(SharedMessages.IsRequired))]
        [Display(ResourceType = typeof(titles), Name = nameof(titles.ArabicName))]
        public string ArabicName { get; set; }

        //[Required(ErrorMessageResourceType = typeof(SharedMessages),
        //        ErrorMessageResourceName = nameof(SharedMessages.IsRequired))]
        [Display(ResourceType = typeof(titles), Name = nameof(titles.EnglishName))]
        public string EnglishName { get; set; }

        [Required(ErrorMessageResourceType = typeof(SharedMessages),
        ErrorMessageResourceName = nameof(SharedMessages.IsRequired))]
        [Display(ResourceType = typeof(titles), Name = nameof(titles.Department))]
        public int DepartmentID { get; set; }

        [Required(ErrorMessageResourceType = typeof(SharedMessages),
        ErrorMessageResourceName = nameof(SharedMessages.IsRequired))]
        [Display(ResourceType = typeof(titles), Name = nameof(titles.Type))]
        public Types Type { get; set; }

        public IEnumerable<DeviceGridRow> DeviceGrid { get; set; } = new HashSet<DeviceGridRow>();
        public IEnumerable<DeviceListItem> DeviceList { get; set; } = new HashSet<DeviceListItem>();
        public IEnumerable<DepartmentListItem> DepartmentList { get; set; } = new HashSet<DepartmentListItem>();

        public bool CanCreate { get; set; }
        public bool CanEdit { get; set; }
        public bool CanDelete { get; set; }
    }

    public class DeviceListItem
    {
        public int DeviceID { get; set; }
        public string Name { get; set; }
    }

    public class DeviceGridRow
    {
        public int DeviceID { get; set; }
        public string ArabicName { get; set; }
        public string EnglishName { get; set; }
        public int DepartmentID { get; set; }
        public string  DepartmentName { get; set; }
        public Types Type { get; set; }
    }
}