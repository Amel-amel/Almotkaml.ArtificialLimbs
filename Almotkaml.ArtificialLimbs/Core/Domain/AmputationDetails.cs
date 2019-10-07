using Almotkaml.ArtificialLimbs.Core.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Almotkaml.ArtificialLimbs.Core.Domain
{
    public class AmputationDetails
    {
        public int AmputationDetailsID { get; set; }
        public int AmputationStatuesID { get; set; } //رقم الحالة
        public AmputationStatues amputationStatues { get; set; }

        public MeasurementModel MeasurementModel { get; set; } //نموذج القياس
        public MeasurementNumberModel Part { get; set; } // (رقم المنطقة(طول/قطر
        //public AmputationPart AmputationPart { get; set; }

        public double? PartMeasure { get; set; } //قياس المنطقة
        public int? shoesNo { get; set; } // رقم الحذاء
        public string Note { get; set; }
        public static AmputationDetails New(
                int _AmputationStatuesID,
                 MeasurementModel _MeasurementModel,
                 MeasurementNumberModel _MeasurementNumberModel,
                 AmputationPart _AmputationPart,
                 double? _PartMeasure,
                 int? _shoesNo
                 , string _note)
        {
            Check.MoreThanZero(_AmputationStatuesID, nameof(_AmputationStatuesID));

            var dailyDetails = new AmputationDetails()
            {
                AmputationStatuesID = _AmputationStatuesID,
                MeasurementModel = _MeasurementModel,
                Part = _MeasurementNumberModel,
                //AmputationPart = _AmputationPart,
                PartMeasure = _PartMeasure,
                shoesNo = _shoesNo,
                Note = _note,
            };

            return dailyDetails;
        }

        public void Modify(int _AmputationStatuesID,
                 MeasurementModel _MeasurementModel,
                 MeasurementNumberModel _MeasurementNumberModel,
                 AmputationPart _AmputationPart,
                 double? _PartMeasure,
                 int? _shoesNo
                 , string _note)
        {
            Check.MoreThanZero(_AmputationStatuesID, nameof(_AmputationStatuesID));

            AmputationStatuesID = _AmputationStatuesID;
            MeasurementModel = _MeasurementModel;
            Part = _MeasurementNumberModel;
           // AmputationPart = _AmputationPart;
            PartMeasure = _PartMeasure;
            shoesNo = _shoesNo;
            Note = _note;

        }
    }
}