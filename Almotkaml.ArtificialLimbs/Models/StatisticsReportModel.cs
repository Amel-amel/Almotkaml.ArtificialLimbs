using Almotkaml.ArtificialLimbs.Core.Enum;
using Almotkaml.ArtificialLimbs.Core.Resources;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Almotkaml.ArtificialLimbs.Models
{
    public class StatisticsReportModel
    {
        [Required(ErrorMessageResourceType = typeof(SharedMessages),
        ErrorMessageResourceName = nameof(SharedMessages.IsRequired))]
        [Display(ResourceType = typeof(titles), Name = nameof(titles.DateFrom))]
        public string DateFrom { get; set; }
        [Required(ErrorMessageResourceType = typeof(SharedMessages),
        ErrorMessageResourceName = nameof(SharedMessages.IsRequired))]
        [Display(ResourceType = typeof(titles), Name = nameof(titles.DateTo))]
        public string DateTo { get; set; }
        [Required(ErrorMessageResourceType = typeof(SharedMessages),
        ErrorMessageResourceName = nameof(SharedMessages.IsRequired))]
        [Display(ResourceType = typeof(titles), Name = nameof(titles.Type))]
        public Types StatuesTypes { get; set; } // يومية أو بـتـر
        [Required(ErrorMessageResourceType = typeof(SharedMessages),
        ErrorMessageResourceName = nameof(SharedMessages.IsRequired))]
        [Display(ResourceType = typeof(titles), Name = nameof(titles.Age))]
        public int Age { get; set; }
        public string Above { get; set; }
        public string Under { get; set; }
        [Display(ResourceType = typeof(titles), Name = nameof(titles.Device))]
        public int DeviceID { get; set; } //الجهاز
        [Display(ResourceType = typeof(titles), Name = nameof(titles.AmputationPart))]
        public AmputationPart AmputationPart { get; set; } //جهة البتر
        [Display(ResourceType = typeof(titles), Name = nameof(titles.AmputationReason))]
        public string AmputationReason { get; set; } //سبب البتر
        [Display(ResourceType = typeof(titles), Name = nameof(titles.AmputationName))]
        public int AmputationTypeID { get; set; } //نوع البتر
        [Display(ResourceType = typeof(titles), Name = nameof(titles.AmputationName))]
        public string AmputationType { get; set; } //نوع البتر
        [Display(ResourceType = typeof(titles), Name = nameof(titles.Device))]
        public string DeviceName { get; set; }
        [Display(ResourceType = typeof(titles), Name = nameof(titles.males))]
        public string AboveAgeM { get; set; }//----فوق العمر المحدد(Male) 
        [Display(ResourceType = typeof(titles), Name = nameof(titles.females))]
        public string AboveAgeF { get; set; }//----فوق العمر المحدد(FeMale)
        [Display(ResourceType = typeof(titles), Name = nameof(titles.males))]
        public string UnderAgeM { get; set; }//----تحت العمر المحدد(Male)
        [Display(ResourceType = typeof(titles), Name = nameof(titles.females))]
        public string UnderAgeF { get; set; }//----تحت العمر المحدد(FeMale)
        [Display(ResourceType = typeof(titles), Name = nameof(titles.Total))]
        public int Total { get; set; }
        public IEnumerable<DStatisticsReportGridRow> DStatisticsReportGrid { get; set; } = new HashSet<DStatisticsReportGridRow>();
        public IEnumerable<AStatisticsReportGridRow> AStatisticsReportGrid { get; set; } = new HashSet<AStatisticsReportGridRow>();
        public IEnumerable<AmputationTypesListItem> AmputationTypesList { get; set; } = new HashSet<AmputationTypesListItem>();
        public IEnumerable<DeviceListItem> DeviceList { get; set; } = new HashSet<DeviceListItem>();
        public IEnumerable<ReportDailyGridRow> ReportDailyGrid { get; set; } = new HashSet<ReportDailyGridRow>();
        public IEnumerable<AmputationStatuesGridRow> AmputationStatuesGrid = new HashSet<AmputationStatuesGridRow>();
        public IEnumerable<DailyStatuesGridRow> DailyStatuesGrid = new HashSet<DailyStatuesGridRow>();




    }
    public class DStatisticsReportGridRow
    {
        public int DeviceID { get; set; }
        public string DeviceName { get; set; }
        public int AboveAgeM { get; set; }//----فوق العمر المحدد(Male) 
        public int AboveAgeF { get; set; }//----فوق العمر المحدد(FeMale)
        public int UnderAgeM { get; set; }//----تحت العمر المحدد(Male)
        public int UnderAgeF { get; set; }//----تحت العمر المحدد(FeMale)
        public string ReceiptDate { get; set; }
        public int Total { get; set; }

    }
    public class AStatisticsReportGridRow
    {
        public int DeviceID { get; set; }
        public string DeviceName { get; set; }
        public int ReasonID { get; set; }
        public string AmputationReason { get; set; }
        public string ReceiptDate { get; set; }
        public int AboveAgeM { get; set; }//----فوق العمر المحدد(Male) 
        public int AboveAgeF { get; set; }//----فوق العمر المحدد(FeMale)
        public int UnderAgeM { get; set; }//----تحت العمر المحدد(Male)
        public int UnderAgeF { get; set; }//----تحت العمر المحدد(FeMale)
        public int Total { get; set; }
    }

}