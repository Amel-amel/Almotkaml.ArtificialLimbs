using Almotkaml.ArtificialLimbs.Core.Enum;
using Almotkaml.ArtificialLimbs.Core.Resources;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Almotkaml.ArtificialLimbs.Models
{
    public class StatuesReportModel
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

        [Display(ResourceType = typeof(titles), Name = nameof(titles.AmputationName))]
        public int AmputationTypeID { get; set; } //نوع البتر
        [Display(ResourceType = typeof(titles), Name = nameof(titles.AmputationPart))]
        public AmputationPart AmputationPart { get; set; } //جهة البتر
        [Display(ResourceType = typeof(titles), Name = nameof(titles.AmputationReason))]
        public string AmputationReason { get; set; } //سبب البتر
        [Display(ResourceType = typeof(titles), Name = nameof(titles.AmputationDate))]
        public string AmputationDate { get; set; } //تاريخ البتر
        [Display(ResourceType = typeof(titles), Name = nameof(titles.ShoeNO))]
        public IEnumerable<StatuesReportGridRow> StatuesReportGrid { get; set; } = new HashSet<StatuesReportGridRow>();
        public IEnumerable<AmputationTypesListItem> AmputationTypesList { get; set; } = new HashSet<AmputationTypesListItem>();

        //****************************************************
        public bool PatientNameCheck { get; set; }
        public bool GenderCheck { get; set; }
        public bool BirthDateCheck { get; set; }
        public bool PhoneNumberCheck { get; set; }
        public bool NationalityNumberCheck { get; set; }
        public bool NationalityCheck { get; set; }
        public bool CityCheck { get; set; }
        public bool PlaceCheck { get; set; }
        public bool RegistrationDateCheck { get; set; }
        //--------------------------------------------------
        public bool TechnicalNameCheck { get; set; }//فني
        public bool AssistantNameCheck { get; set; }//مساعد فني
        public bool DeviceNameCheck { get; set; }
        public bool MeasurementDateCheck { get; set; }
        public bool FirstTestDateCheck { get; set; }
        public bool SecondTestDateCheck { get; set; }
        public bool ReceiptDateCheck { get; set; }
        public int? ReferenceNoCheck { get; set; }
        public bool AmputationTypeCheck { get; set; } //نوع البتر
        public bool AmputationPartCheck { get; set; } //جهة البتر
        public bool AmputationReasonCheck { get; set; } //سبب البتر
        public bool AmputationDateCheck { get; set; } //تاريخ البتر
        public bool ShoeNOCheck { get; set; }
        public bool NoteCheck { get; set; }

        //*****************************************************
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
        public int? ReferenceNo { get; set; }
        public string AmputationType { get; set; } //نوع البتر
    
        public int ShoeNO { get; set; }
        public string Note { get; set; }

    }
    public class StatuesReportGridRow
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
        public int? ReferenceNo { get; set; }
        public string AmputationType { get; set; } //نوع البتر
        public string AmputationPart { get; set; } //جهة البتر
        public string AmputationReason { get; set; } //سبب البتر
        public string AmputationDate { get; set; } //تاريخ البتر
        public int ShoeNO { get; set; }
        public string Note { get; set; }

    }
 
}