using Almotkaml.ArtificialLimbs.Core.Enum;
using Almotkaml.ArtificialLimbs.Core.Resources;
using Almotkaml.Attributes;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Almotkaml.ArtificialLimbs.Models
{
    public class AmputationStatuesModel
    {
        public int AmputationStatuesID { get; set; }

        [Required(ErrorMessageResourceType = typeof(SharedMessages),
        ErrorMessageResourceName = nameof(SharedMessages.IsRequired))]
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
        [Display(ResourceType = typeof(titles), Name = nameof(titles.FirstTestDate))]
        public string FirstTestDate { get; set; } // تاريخ التجربة الاولى
        [Display(ResourceType = typeof(titles), Name = nameof(titles.SecondTestDate))]
        public string SecondTestDate { get; set; } // التجربة الثانية

        [Display(ResourceType = typeof(titles), Name = nameof(titles.ReceiptDate))]
        public string ReceiptDate { get; set; } //تاريخ الاستلام

        [Display(ResourceType = typeof(titles), Name = nameof(titles.ReferenceNo))]
        public int? ReferenceNo { get; set; } //الرقم الاشاري

        [Display(ResourceType = typeof(titles), Name = nameof(titles.Note))]
        public string Note { get; set; } // ملاحظات

        [Required(ErrorMessageResourceType = typeof(SharedMessages),
        ErrorMessageResourceName = nameof(SharedMessages.IsRequired))]
        [Display(ResourceType = typeof(titles), Name = nameof(titles.AmputationName))]
        public int AmputationTypeID { get; set; } //نوع البتر
        [Required(ErrorMessageResourceType = typeof(SharedMessages),
        ErrorMessageResourceName = nameof(SharedMessages.IsRequired))]
        [Display(ResourceType = typeof(titles), Name = nameof(titles.AmputationPart))]
        public AmputationPart AmputationPart { get; set; } //جهة البتر
        [Required(ErrorMessageResourceType = typeof(SharedMessages),
        ErrorMessageResourceName = nameof(SharedMessages.IsRequired))]
        [Display(ResourceType = typeof(titles), Name = nameof(titles.AmputationReason))]
        public string AmputationReason { get; set; } //سبب البتر
        public int AmputationReasonID { get; set; } //سبب البتر
        [Required(ErrorMessageResourceType = typeof(SharedMessages),
        ErrorMessageResourceName = nameof(SharedMessages.IsRequired))]
        [Display(ResourceType = typeof(titles), Name = nameof(titles.AmputationDate))]
        public string AmputationDate { get; set; } //تاريخ البتر
        [Required(ErrorMessageResourceType = typeof(SharedMessages),
        ErrorMessageResourceName = nameof(SharedMessages.IsRequired))]
        [Display(ResourceType = typeof(titles), Name = nameof(titles.ShoeNO))]
        public bool SetRefNO { get; set; }

        [Date]
        [Display(ResourceType = typeof(SharedTitles), Name = nameof(SharedTitles.FromDate))]
        public string DateFrom { get; set; }
        [Date]
        [Display(ResourceType = typeof(SharedTitles), Name = nameof(SharedTitles.ToDate))]
        public string DateTo { get; set; }


        public AmputationDetailsModel Details { get; set; }

        public IEnumerable<AmputationStatuesGridRow> AmputationStatuesGrid = new HashSet<AmputationStatuesGridRow>();
        public IEnumerable<PatientGridRow> PatientGrid = new HashSet<PatientGridRow>();
        public IEnumerable<EmployeeGridRow> EmployeeGrid = new HashSet<EmployeeGridRow>();
        public IEnumerable<DeviceListItem> DeviceList { get; set; } = new HashSet<DeviceListItem>();
        public IEnumerable<AmputationReasonGridRow> AmputationReasonGrid { get; set; } = new HashSet<AmputationReasonGridRow>();
        public IEnumerable<AmputationReasonList> AmputationReasonList { get; set; } = new HashSet<AmputationReasonList>();
        public IEnumerable<AmputationTypesListItem> AmputationTypesList { get; set; } = new HashSet<AmputationTypesListItem>();

        public bool CanCreate { get; set; }
        public bool CanEdit { get; set; }
        public bool CanDelete { get; set; }

    }

    public class AmputationStatuesGridRow
    {
        public int AmputationStatuesID { get; set; }
        public int PatientID { get; set; } 
        public string PatientName { get; set; }
        public int? TechnicalID { get; set; }
        public string TechnicalName { get; set; }
        public int? TechnicalAssistantID { get; set; }
        public string AssistantName { get; set; }
        public int DeviceID { get; set; } 
        public string DeviceName { get; set; }
        public string MeasurementDate { get; set; }
        public string FirstTestDate { get; set; }
        public string SecondTestDate { get; set; }
        public string ReceiptDate { get; set; } 
        public int? ReferenceNo { get; set; }
        public int AmputationTypeID { get; set; } //نوع البتر
        public AmputationPart AmputationPart { get; set; } //جهة البتر
        public int AmputationReasonID { get; set; } 
        public string AmputationReason { get; set; } //سبب البتر
        public string AmputationDate { get; set; } //تاريخ البتر
        public int ShoeNO { get; set; }
        public string Note { get; set; }
    }

    public class AmputationReasonGridRow
    {
        public int AmputationReasonID { get; set; }
        public string Reason { get; set; }
    }

    public class AmputationReasonList
    {
        public int AmputationReasonID { get; set; }
        public string Reason { get; set; }
    }
    }