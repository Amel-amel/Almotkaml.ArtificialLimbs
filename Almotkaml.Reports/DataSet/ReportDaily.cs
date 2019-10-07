using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Almotkaml.Reports
{
    public class ReportDaily
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
        //-------------------------------------------------------
        public string MeasurementModel { get; set; } //نموذج القياس
        public string Part { get; set; } // (رقم المنطقة(طول/قطر
        public double? PartMeasure { get; set; } //قياس المنطقة
        public int? ShoeNO { get; set; } // رقم الحذاء

    }
}
