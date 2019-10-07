using Almotkaml.ArtificialLimbs.Core.Enum;
using Almotkaml.ArtificialLimbs.Core.Resources;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Almotkaml.ArtificialLimbs.Models
{
    public class ReportsModel
    {
        public bool CheckMeasurementModel { get; set; }
        public bool CheckPart { get; set; }
        public bool CheckPartMeasure { get; set; }
        public bool CheckShoeNO { get; set; }


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

        [Display(ResourceType = typeof(titles), Name = nameof(titles.AmputationPart))]
        public AmputationPart AmputationPart { get; set; } //جهة البتر
        [Display(ResourceType = typeof(titles), Name = nameof(titles.AmputationReason))]
        public string AmputationReason { get; set; } //سبب البتر
        [Display(ResourceType = typeof(titles), Name = nameof(titles.AmputationDate))]
        public string AmputationDate { get; set; } //تاريخ البتر
        [Display(ResourceType = typeof(titles), Name = nameof(titles.ShoeNO))]
        public IEnumerable<ReportDailyGridRow> ReportDailyGrid { get; set; } = new HashSet<ReportDailyGridRow>();
        [Display(ResourceType = typeof(titles), Name = nameof(titles.AmputationName))]
        public int AmputationTypeID { get; set; } //نوع البتر
        public IEnumerable<ReportAmputationGridRow> ReportAmputationGrid { get; set; } = new HashSet<ReportAmputationGridRow>();
        public IEnumerable<AmputationTypesListItem> AmputationTypesList { get; set; } = new HashSet<AmputationTypesListItem>();

        [Display(ResourceType = typeof(titles), Name = nameof(titles.PatientName))]
        public string PatientName { get; set; }
        [Display(ResourceType = typeof(titles), Name = nameof(titles.Gender))]
        public Gender Gender { get; set; }
        [Display(ResourceType = typeof(titles), Name = nameof(titles.YearOfBirth))]
        public string BirthDate { get; set; }
        [Display(ResourceType = typeof(titles), Name = nameof(titles.PhoneNO))]
        public string PhoneNumber { get; set; }
        [Display(ResourceType = typeof(titles), Name = nameof(titles.NationalityNumber))]
        public string NationalityNumber { get; set; }
        [Display(ResourceType = typeof(titles), Name = nameof(titles.Nationality))]
        public string Nationality { get; set; }
        [Display(ResourceType = typeof(titles), Name = nameof(titles.Address))]
        public string City { get; set; }
        [Display(ResourceType = typeof(titles), Name = nameof(titles.Place))]
        public string Place { get; set; }
        [Display(ResourceType = typeof(titles), Name = nameof(titles.RegistrationDate))]
        public string RegistrationDate { get; set; }
        //-------------------------------------------------------
        [Display(ResourceType = typeof(titles), Name = nameof(titles.EmployeeName))]
        public string TechnicalName { get; set; }//فني
        [Display(ResourceType = typeof(titles), Name = nameof(titles.EmployeeName))]
        public string AssistantName { get; set; }//مساعد فني
        [Display(ResourceType = typeof(titles), Name = nameof(titles.Device))]
        public string DeviceName { get; set; }
        [Display(ResourceType = typeof(titles), Name = nameof(titles.MeasurementDate))]
        public string MeasurementDate { get; set; }
        [Display(ResourceType = typeof(titles), Name = nameof(titles.FirstTestDate))]
        public string FirstTestDate { get; set; }
        [Display(ResourceType = typeof(titles), Name = nameof(titles.SecondTestDate))]
        public string SecondTestDate { get; set; }
        [Display(ResourceType = typeof(titles), Name = nameof(titles.ReceiptDate))]
        public string ReceiptDate { get; set; }
        [Display(ResourceType = typeof(titles), Name = nameof(titles.ReferenceNo))]
        public int? ReferenceNo { get; set; }
        [Display(ResourceType = typeof(titles), Name = nameof(titles.AmputationName))]
        public string AmputationType { get; set; } //نوع البتر
        [Display(ResourceType = typeof(titles), Name = nameof(titles.ShoeNO))]
        public int ShoeNO { get; set; }
        [Display(ResourceType = typeof(titles), Name = nameof(titles.Note))]
        public string Note { get; set; }

        [Display(ResourceType = typeof(titles), Name = nameof(titles.Part))]
        public MeasurementModel MeasurementModel { get; set; } //نموذج القياس

        [Display(ResourceType = typeof(titles), Name = nameof(titles.PartNO))]
        public MeasurementNumberModel Part { get; set; } // (رقم المنطقة(طول/قطر

        [Display(ResourceType = typeof(titles), Name = nameof(titles.PartMeasure))]
        public double? PartMeasure { get; set; } //قياس المنطقة

    }
    public class ReportDailyGridRow
    {
        public string PatientName { get; set; }
        public string Gender { get; set; }
        public string BirthDate { get; set; }
        public string PhoneNumber { get; set; }
        public string NationalityNumber { get; set; }
        public string Nationality { get; set; }
        public string City { get; set; }
        public string Place { get; set; }
        public string RegistrationDate { get; set; }
        //-------------------------------------------------------
        public string TechnicalName { get; set; }//فني
        public string AssistantName { get; set; }//مساعد فني
        public string DeviceName { get; set; }
        public string MeasurementDate { get; set; }
        public string ReceiptDate { get; set; }
        public string ReferenceNo { get; set; }
        public string Note { get; set; }
        //----------------------------تفاصيل القياس---------------------------
        public string MeasurementModel { get; set; } //نموذج القياس
        public string Part { get; set; } // (رقم المنطقة(طول/قطر
        public double? PartMeasure { get; set; } //قياس المنطقة
        public int? ShoeNO { get; set; } // رقم الحذاء

    }
    public class ReportAmputationGridRow
    {
        public string PatientName { get; set; }
        public string Gender { get; set; }
        public string BirthDate { get; set; }
        public string PhoneNumber { get; set; }
        public string NationalityNumber { get; set; }
        public string Nationality { get; set; }
        public string City { get; set; }
        public string Place { get; set; }
        public string RegistrationDate { get; set; }
        //-------------------------------------------------------
        public string TechnicalName { get; set; }//فني
        public string AssistantName { get; set; }//مساعد فني
        public string DeviceName { get; set; }
        public string MeasurementDate { get; set; }
        public string FirstTestDate { get; set; }
        public string SecondTestDate { get; set; }
        public string ReceiptDate { get; set; }
        public string ReferenceNo { get; set; }
        public string AmputationType { get; set; } //نوع البتر
        public string AmputationPart { get; set; } //جهة البتر
        public string AmputationReason { get; set; } //سبب البتر
        public string AmputationDate { get; set; } //تاريخ البتر
        public string ShoeNO { get; set; }
        public string Note { get; set; }

    }
}