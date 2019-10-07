using Almotkaml.ArtificialLimbs.Core.Resources;
using Almotkaml.Attributes;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Almotkaml.ArtificialLimbs.Models
{
    public class DailyStatuesModel
    {
        public int DailyStatuesID { get; set; }

        //[Required(ErrorMessageResourceType = typeof(SharedMessages),
        //ErrorMessageResourceName = nameof(SharedMessages.IsRequired))]
        [Display(ResourceType = typeof(titles), Name = nameof(titles.PatientName))]
        public int PatientID { get; set; } //رقم المريض

        [Display(ResourceType = typeof(titles), Name = nameof(titles.PatientName))]
        public string PatientName { get; set; }
        
        [Display(ResourceType = typeof(titles), Name = nameof(titles.EmployeeName))]
        public int? TechnicalID { get; set; } // رقم الفني
        [Display(ResourceType = typeof(titles), Name = nameof(titles.EmployeeName))]
        public string TechnicalName { get; set; }

        [Display(ResourceType = typeof(titles), Name = nameof(titles.Assistant))]
        public int? TechnicalAssistantID { get; set; } // مساعد الفني
        [Display(ResourceType = typeof(titles), Name = nameof(titles.Assistant))]
        public string AssistantName { get; set; }

        [Required(ErrorMessageResourceType = typeof(SharedMessages),
        ErrorMessageResourceName = nameof(SharedMessages.IsRequired))]
        [Display(ResourceType = typeof(titles), Name = nameof(titles.Device))]
        public int DeviceID { get; set; } //الجهاز
        [Display(ResourceType = typeof(titles), Name = nameof(titles.Device))]
        public string DeviceName { get; set; }

        [Display(ResourceType = typeof(titles), Name = nameof(titles.MeasurementDate))]
        public string MeasurementDate { get; set; } //تاريخ القياس

        [Display(ResourceType = typeof(titles), Name = nameof(titles.ReceiptDate))]
        public string ReceiptDate { get; set; } // تاريخ التسليم

        [Display(ResourceType = typeof(titles), Name = nameof(titles.ReferenceNo))]
        public int? ReferenceNo { get; set; } //الرقم الاشاري

        [Display(ResourceType = typeof(titles), Name = nameof(titles.Note))]
        public string Note { get; set; } // ملاحظات

        public DailyDetailsModel Details { get; set; }
        public bool SetRefNO { get; set; }
        [Date]
        [Display(ResourceType = typeof(SharedTitles), Name = nameof(SharedTitles.FromDate))]
        public string DateFrom { get; set; }
        [Date]
        [Display(ResourceType = typeof(SharedTitles), Name = nameof(SharedTitles.ToDate))]
        public string DateTo { get; set; }

        public IEnumerable<DailyStatuesGridRow> DailyStatuesGrid = new HashSet<DailyStatuesGridRow>();
        public IEnumerable<PatientGridRow> PatientGrid = new HashSet<PatientGridRow>();
        public IEnumerable<EmployeeGridRow> EmployeeGrid = new HashSet<EmployeeGridRow>();
        public IEnumerable<DeviceListItem> DeviceList { get; set; } = new HashSet<DeviceListItem>();

        public bool CanCreate { get; set; }
        public bool CanEdit { get; set; }
        public bool CanDelete { get; set; }

    }

    public class DailyStatuesGridRow
    {
        public int DailyStatuesID { get; set; }
        public int PatientIID { get; set; } 
        public string PatientName { get; set; }
        public int? TechnicalID { get; set; }
        public string TechnicalName { get; set; }
        public int? TechnicalAssistantID { get; set; }
        public string AssistantName { get; set; }
        public int DeviceID { get; set; } 
        public string DeviceName { get; set; }
        public string MeasurementDate { get; set; } 
        public string ReceiptDate { get; set; } 
        public int? ReferenceNo { get; set; } 
        public string Note { get; set; }
    }

}