using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Almotkaml.Reports
{
    public class ReportAmputation
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
