using Almotkaml.ArtificialLimbs.Core.Enum;
using Almotkaml.ArtificialLimbs.Core.Resources;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Security.AccessControl;
using System.Web;

namespace Almotkaml.ArtificialLimbs.Models
{
    public class AchievementsModel
    {
        [Required(ErrorMessageResourceType = typeof(SharedMessages),
        ErrorMessageResourceName = nameof(SharedMessages.IsRequired))]
        [Display(ResourceType = typeof(titles), Name = nameof(titles.DateFrom))]
        public string DateFrom { get; set; }
        [Required(ErrorMessageResourceType = typeof(SharedMessages),
        ErrorMessageResourceName = nameof(SharedMessages.IsRequired))]
        [Display(ResourceType = typeof(titles), Name = nameof(titles.DateTo))]
        public string DateTo { get; set; }
        
        [Display(ResourceType = typeof(titles), Name = nameof(titles.Type))]
        public Types? StatuesTypes { get; set; } // يومية أو بـتـر
        
        [Display(ResourceType = typeof(titles), Name = nameof(titles.Device))]
        public int? DeviceID { get; set; } //الجهاز
        
        [Display(ResourceType = typeof(titles), Name = nameof(titles.Total))]
        public int Total { get; set; }
        public IEnumerable<DeviceListItem> DeviceList { get; set; } = new HashSet<DeviceListItem>();
        public IEnumerable<AchievementReportGridRow> AchievementReportGrid { get; set; } = new HashSet<AchievementReportGridRow>();
     



    }
    public class AchievementReportGridRow
    {
        public int DeviceID { get; set; }
        public string DeviceName { get; set; }
        public string ReceiptDate { get; set; }
        public int Total { get; set; }

    }

}