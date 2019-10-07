using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Almotkaml.Reports.DataSet
{
    public class DynamicReport
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

        public int? ReferenceNo { get; set; }

        public int PatientID { get; set; } 
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

        public int DeviceID { get; set; } 
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
