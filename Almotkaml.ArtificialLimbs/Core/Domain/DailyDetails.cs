using Almotkaml.ArtificialLimbs.Core.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Almotkaml.ArtificialLimbs.Core.Domain
{
    public class DailyDetails
    {
        public int DailyDetailsID { get; set; }
        public int DailyStatuesID { get; set; } //رقم الحالة
        public DailyStatues dailyStatues { get; set; }

        public MeasurementModel MeasurementModel { get; set; } //نموذج القياس
        public MeasurementNumberModel Part { get; set; } // (رقم المنطقة(طول/قطر
        //public AmputationPart AmputationPart { get; set; }
        public string Note { get; set; }
        public double? PartMeasure { get; set; } //قياس المنطقة
        public int? shoesNo { get; set; } // رقم الحذاء

        public static DailyDetails New(
                int _dailyStatuesID,
                 MeasurementModel _MeasurementModel,
                 MeasurementNumberModel _MeasurementNumberModel,
                 string  _Note,
                 double? _PartMeasure,
                 int? _shoesNo)
        {
            Check.MoreThanZero(_dailyStatuesID, nameof(_dailyStatuesID));

            var dailyDetails = new DailyDetails()
            {
                DailyStatuesID = _dailyStatuesID,
                MeasurementModel = _MeasurementModel,
                Part = _MeasurementNumberModel,
                Note = _Note,
                PartMeasure = _PartMeasure,
                shoesNo = _shoesNo,
            };

            return dailyDetails;
        }

        public void Modify(int _dailyStatuesID,
                 MeasurementModel _MeasurementModel,
                 MeasurementNumberModel _MeasurementNumberModel,
                 string _Note,
                 double? _PartMeasure,
                 int? _shoesNo)
        {
            Check.MoreThanZero(_dailyStatuesID, nameof(_dailyStatuesID));

            DailyStatuesID = _dailyStatuesID;
            MeasurementModel = _MeasurementModel;
            Part = _MeasurementNumberModel;
            Note = _Note;
            PartMeasure = _PartMeasure;
            shoesNo = _shoesNo;

        }

    }
}