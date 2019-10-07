using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Almotkaml.ArtificialLimbs.Core.Domain
{
    public class DailyStatues
    {
        public int DailyStatuesID { get; set; }
        public int PatientID { get; set; } //رقم المريض
        public Patient patient { get; set; }
        //[ForeignKey("Employee"),Column("EmployeeID")]
        public int? TechnicalID { get; set; } // رقم الفني
        public int? TechnicalAssistantID { get; set; } // مساعد الفني
        public int DeviceID { get; set; } //الجهاز
        public Device device { get; set; }
        public string MeasurementDate { get; set; } //تاريخ القياس
        public string ReceiptDate { get; set; } // تاريخ التسليم
        public int? ReferenceNo { get; set; } //الرقم الاشاري
        public string Note { get; set; } // ملاحظات

        public virtual Employee TechnicalAssistant { get; set; }
        public virtual Employee Technical { get; set; }

        public ICollection<DailyDetails> DailyDetails { get; } = new HashSet<DailyDetails>();

        public static DailyStatues New(int _patientIID, int _deviceID)
        {
            Check.MoreThanZero(_patientIID, nameof(_patientIID));
            Check.MoreThanZero(_deviceID, nameof(_deviceID));

            var dailyStatues = new DailyStatues()
            {
                PatientID = _patientIID,
                DeviceID = _deviceID,
            };

            return dailyStatues;
        }

        public static DailyStatues New(int _patientIID, int? _technicalID, int? _technicalAssistantID, int _deviceID,
                                     string _measurementDate, string _receiptDate, int? _referenceNo, string _note)
        {
            Check.MoreThanZero(_patientIID, nameof(_patientIID));
            Check.MoreThanZero(_deviceID, nameof(_deviceID));

            var dailyStatues = new DailyStatues()
            {
                PatientID =_patientIID,
                TechnicalID =_technicalID,
                TechnicalAssistantID =_technicalAssistantID ,
                DeviceID =_deviceID ,
                MeasurementDate =_measurementDate,
                ReceiptDate =_receiptDate,
                ReferenceNo=_referenceNo,
                Note =_note,
            };

            return dailyStatues;
        }

        public void Modify(int _patientIID, int? _technicalID, int? _technicalAssistantID, int _deviceID,
                                     string _measurementDate, string _receiptDate, int? _referenceNo, string _note)
        {
            Check.MoreThanZero(_patientIID, nameof(_patientIID));
            Check.MoreThanZero(_deviceID, nameof(_deviceID));

            PatientID = _patientIID;
            TechnicalID = _technicalID;
            TechnicalAssistantID = _technicalAssistantID;
            DeviceID = _deviceID;
            MeasurementDate = _measurementDate;
            ReceiptDate = _receiptDate;
            ReferenceNo = _referenceNo;
            Note = _note;

        }

        public void Receipt()
        {
            ReceiptDate = DateTime.Now.ToShortDateString();
        }
    }
}