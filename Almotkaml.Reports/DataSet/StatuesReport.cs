using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Almotkaml.Reports
{
    public class StatuesReport
    {

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
        public string AmputationPart { get; set; } //جهة البتر
        public string AmputationReason { get; set; } //سبب البتر
        public string AmputationDate { get; set; } //تاريخ البتر
        public int ShoeNO { get; set; }
        public string Note { get; set; }
    }
}
