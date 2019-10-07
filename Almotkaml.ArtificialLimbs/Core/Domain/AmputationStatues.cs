using Almotkaml.ArtificialLimbs.Core.Enum;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Almotkaml.ArtificialLimbs.Core.Domain
{
    public class AmputationStatues
    {
        public int AmputationStatuesID { get; set; }
        public int PatientID { get; set; } //رقم المريض
        public Patient patient { get; set; }
        //[ForeignKey("Employee"), Column("EmployeeID")]
        public int? TechnicalID { get; set; } //رقم الفني
        public Employee Technical { get; set; }
        public int? TechnicalAssistantID { get; set; } //مساعد الفني
        public virtual Employee TechnicalAssistant { get; set; }
        public int DeviceID { get; set; } //الجهاز
        public Device device { get; set; }
        public string  MeasurementDate { get; set; } //تاريخ القياس
        public string FirstTestDate { get; set; } // تاريخ التجربة الاولى
        public string SecondTestDate { get; set; } // التجربة الثانية
        public string ReceiptDate { get; set; } //تاريخ الاستلام
        public int? ReferenceNo { get; set; } //الرقم الاشاري
        public int AmputationTypeID { get; set; } //نوع البتر
        public AmputationTypes amputationType { get; set; }
        public AmputationPart AmputationPart { get; set; } //جهة البتر
        public AmputationReason AmputationReason { get; set; } //سبب البتر
        public int AmputationReasonID { get; set; }
        public string AmputationDate { get; set; } //تاريخ البتر
        public string  Note { get; set; }// ملاحظات

        public ICollection<AmputationDetails> AmputationDetails { get; } = new HashSet<AmputationDetails>();


        public static AmputationStatues New(int _patientIID, int _deviceID, int _amputationTypeID)
        {
            Check.MoreThanZero(_patientIID, nameof(_patientIID));
            Check.MoreThanZero(_deviceID, nameof(_deviceID));

            var amputationStatues = new AmputationStatues()
            {
                PatientID = _patientIID,
                DeviceID = _deviceID,
                AmputationTypeID= _amputationTypeID,
            };

            return amputationStatues;
        }
        public static AmputationStatues New(int _patientIID, int? _technicalID, int? _technicalAssistantID, int _deviceID,
                                     string _measurementDate, string _firstTestDate, string _secondTestDate, string _receiptDate, int? _referenceNo,
                                     int _amputationTypeID, AmputationPart _amputationPart, int _amputationReasonID,string _amputationDate, string _note)
        {
            Check.MoreThanZero(_patientIID, nameof(_patientIID));
            Check.MoreThanZero(_deviceID, nameof(_deviceID));

            var amputationStatues = new AmputationStatues()
            {
                PatientID = _patientIID,
                TechnicalID = _technicalID,
                TechnicalAssistantID = _technicalAssistantID,
                DeviceID = _deviceID,
                MeasurementDate = _measurementDate,
                FirstTestDate = _firstTestDate,
                SecondTestDate = _secondTestDate,
                ReceiptDate =_receiptDate,
                ReferenceNo = _referenceNo,
                AmputationTypeID =_amputationTypeID,
                AmputationPart =_amputationPart,
                AmputationReasonID= _amputationReasonID,
                AmputationDate = _amputationDate,
                Note = _note,
            };

            return amputationStatues;
        }

        public void Modify(int _patientIID, int? _technicalID, int? _technicalAssistantID, int _deviceID,
                                     string _measurementDate, string _firstTestDate, string _secondTestDate, string _receiptDate, int? _referenceNo,
                                     int _amputationTypeID, AmputationPart _amputationPart, int _amputationReasonID, string _amputationDate, string _note)

        {
            Check.MoreThanZero(_patientIID, nameof(_patientIID));
            Check.MoreThanZero(_deviceID, nameof(_deviceID));

            PatientID = _patientIID;
            TechnicalID = _technicalID;
            TechnicalAssistantID = _technicalAssistantID;
            DeviceID = _deviceID;
            MeasurementDate = _measurementDate;
            FirstTestDate =_firstTestDate;
            SecondTestDate =_secondTestDate;
            ReceiptDate = _receiptDate;
            ReferenceNo = _referenceNo;
            AmputationTypeID = _amputationTypeID;
            AmputationPart = _amputationPart;
            AmputationReasonID = _amputationReasonID;
            AmputationDate = _amputationDate;
            Note = _note;

        }
        public void Receipt()
        {
            ReceiptDate = DateTime.Now.ToShortDateString();
        }
        
        public void Reference(int? referenceON)
        {
            ReferenceNo = referenceON;
        }
    }
}