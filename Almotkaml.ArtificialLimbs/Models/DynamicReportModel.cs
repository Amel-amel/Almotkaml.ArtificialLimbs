using Almotkaml.ArtificialLimbs.Core.Enum;
using Almotkaml.ArtificialLimbs.Core.Resources;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Almotkaml.ArtificialLimbs.Models
{
    public class DynamicReportModel
    {

        public bool CheckReferenceNo { get; set; }

        public bool CheckPatientID { get; set; }
        public bool CheckpatientName { get; set; }
        public bool CheckGender { get; set; }
        public bool CheckBirthDate { get; set; }
        public bool CheckPhoneNumber { get; set; }
        public bool CheckNationalityNumber { get; set; }
        public bool CheckNationality { get; set; }
        public bool CheckCity { get; set; }
        public bool CheckPlace { get; set; }
        public bool CheckRegistrationDate { get; set; }

        public bool CheckTechnicalID { get; set; }
        public bool CheckTechnical { get; set; }

        public bool CheckTechnicalAssistantID { get; set; }
        public bool CheckTechnicalAssistant { get; set; }

        public bool CheckDeviceID { get; set; }
        public bool Checkdevice { get; set; }

        public bool CheckMeasurementDate { get; set; }
        public bool CheckFirstTestDate { get; set; }
        public bool CheckSecondTestDate { get; set; }
        public bool CheckReceiptDate { get; set; }

        public bool CheckamputationType { get; set; }
        public bool CheckAmputationPart { get; set; }
        public bool CheckAmputationReason { get; set; }
        public bool CheckAmputationDate { get; set; }

        public bool CheckMeasurementModel { get; set; }
        public bool CheckPart { get; set; } 
        public bool CheckPartMeasure { get; set; }
        public bool CheckShoeNO { get; set; }

        public bool CheckNote { get; set; }

        //===================================================================

        [Display(ResourceType = typeof(titles), Name = nameof(titles.ReferenceNo))]
        public int? ReferenceNo { get; set; }

        [Display(ResourceType = typeof(titles), Name = nameof(titles.PatientId))]
        public int? PatientID { get; set; }

        [Display(ResourceType = typeof(titles), Name = nameof(titles.Gender))]
        public Gender Gender { get; set; }
        [Display(ResourceType = typeof(titles), Name = nameof(titles.BirthDate))]
        public string BirthDate { get; set; }
        [Display(ResourceType = typeof(titles), Name = nameof(titles.PhoneNO))]
        public string PhoneNumber { get; set; }
        [Display(ResourceType = typeof(titles), Name = nameof(titles.NationalityNumber))]
        public string NationalityNumber { get; set; }
        [Display(ResourceType = typeof(titles), Name = nameof(titles.Nationality))]
        public string Nationality { get; set; }
        [Display(ResourceType = typeof(titles), Name = nameof(titles.Nationality))]
        public string NationalityID { get; set; }
        [Display(ResourceType = typeof(titles), Name = nameof(titles.Address))]
        public string City { get; set; }
        [Display(ResourceType = typeof(titles), Name = nameof(titles.Address))]
        public string CityID { get; set; }
        [Display(ResourceType = typeof(titles), Name = nameof(titles.Address))]
        public string Place { get; set; }
        [Display(ResourceType = typeof(titles), Name = nameof(titles.RegistrationDate))]
        public string RegistrationDate { get; set; }
        [Display(ResourceType = typeof(titles), Name = nameof(titles.RegistrationDateFrom))]
        public string RegistrationDateFrom { get; set; }
        [Display(ResourceType = typeof(titles), Name = nameof(titles.RegistrationDateTo))]
        public string RegistrationDateTo { get; set; }

        [Display(ResourceType = typeof(titles), Name = nameof(titles.EmployeeName))]
        public int? TechnicalID { get; set; }
        [Display(ResourceType = typeof(titles), Name = nameof(titles.EmployeeName))]
        public string Technical { get; set; }
        [Display(ResourceType = typeof(titles), Name = nameof(titles.Assistant))]
        public int? TechnicalAssistantID { get; set; }
        [Display(ResourceType = typeof(titles), Name = nameof(titles.EmployeeName))]
        public string TechnicalAssistant { get; set; }
        [Display(ResourceType = typeof(titles), Name = nameof(titles.Device))]
        public int? DeviceID { get; set; }
        [Display(ResourceType = typeof(titles), Name = nameof(titles.Device))]
        public string device { get; set; }

        [Display(ResourceType = typeof(titles), Name = nameof(titles.MeasurementDate))]
        public string MeasurementDate { get; set; }
        [Display(ResourceType = typeof(titles), Name = nameof(titles.FirstTestDate))]
        public string FirstTestDate { get; set; }
        [Display(ResourceType = typeof(titles), Name = nameof(titles.SecondTestDate))]
        public string SecondTestDate { get; set; }
        [Display(ResourceType = typeof(titles), Name = nameof(titles.ReceiptDate))]
        public string ReceiptDate { get; set; }

        [Display(ResourceType = typeof(titles), Name = nameof(titles.AmputationName))]
        public string amputationType { get; set; }
        [Display(ResourceType = typeof(titles), Name = nameof(titles.AmputationPart))]
        public AmputationPart AmputationPart { get; set; }
        [Display(ResourceType = typeof(titles), Name = nameof(titles.AmputationReason))]
        public string AmputationReason { get; set; }
        [Display(ResourceType = typeof(titles), Name = nameof(titles.AmputationDate))]
        public string AmputationDate { get; set; }

        [Display(ResourceType = typeof(titles), Name = nameof(titles.PatientName))]
        public string PatientName { get; set; }

        [Display(ResourceType = typeof(titles), Name = nameof(titles.Part))]
        public MeasurementModel MeasurementModel { get; set; } //نموذج القياس

        [Display(ResourceType = typeof(titles), Name = nameof(titles.PartNO))]
        public MeasurementNumberModel Part { get; set; } // (رقم المنطقة(طول/قطر

        [Display(ResourceType = typeof(titles), Name = nameof(titles.PartMeasure))]
        public double? PartMeasure { get; set; } //قياس المنطقة

        [Display(ResourceType = typeof(titles), Name = nameof(titles.ShoeNO))]
        public int? ShoeNO { get; set; } // رقم الحذاء

        [Display(ResourceType = typeof(titles), Name = nameof(titles.Note))]
        public string Note { get; set; }

        //===================================================================

        public IEnumerable<PatientGridRow> PatientGrid { get; set; } = new HashSet<PatientGridRow>();
        public IEnumerable<CityListItem> CityList { get; set; } = new HashSet<CityListItem>();
        public IEnumerable<PlaceListItem> PlaceList { get; set; } = new HashSet<PlaceListItem>();
        public IEnumerable<NationalityListItem> NationalityList { get; set; } = new HashSet<NationalityListItem>();
        public IEnumerable<DeviceListItem> DeviceList { get; set; } = new HashSet<DeviceListItem>();
        public IEnumerable<AmputationTypesListItem> AmputationTypesList { get; set; } = new HashSet<AmputationTypesListItem>();
        public IEnumerable<AmputationReasonList> AmputationReasonList { get; set; } = new HashSet<AmputationReasonList>();
        public IEnumerable<DynamicReportGridRow> DynamicReportGrid { get; set; } = new HashSet<DynamicReportGridRow>();
        public IEnumerable<EmployeeGridRow> EmployeeGrid { get; set; }  = new HashSet<EmployeeGridRow>();
    }

    public class DynamicReportGridRow
    {
        public int? ReferenceNo { get; set; }

        public int? PatientID { get; set; }
        public string patientName { get; set; }
        public string Gender { get; set; }
        public string BirthDate { get; set; }
        public string PhoneNumber { get; set; }
        public string NationalityNumber { get; set; }
        public string Nationality { get; set; }
        public string City { get; set; }
        public string Place { get; set; }
        public string RegistrationDate { get; set; }

        public int? TechnicalID { get; set; }
        public string Technical { get; set; }

        public int? TechnicalAssistantID { get; set; }
        public string TechnicalAssistant { get; set; }

        public int? DeviceID { get; set; }
        public string device { get; set; }

        public string MeasurementDate { get; set; }
        public string FirstTestDate { get; set; }
        public string SecondTestDate { get; set; }
        public string ReceiptDate { get; set; }

        public string amputationType { get; set; }
        public string AmputationPart { get; set; }
        public string AmputationReason { get; set; }
        public string AmputationDate { get; set; }

        public string MeasurementModel { get; set; } //نموذج القياس
        public string Part { get; set; } // (رقم المنطقة(طول/قطر
        public double? PartMeasure { get; set; } //قياس المنطقة
        public int? ShoeNO { get; set; } // رقم الحذاء

        public string Note { get; set; }
    }

    }