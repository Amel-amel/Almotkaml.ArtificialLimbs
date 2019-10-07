using Almotkaml.ArtificialLimbs.Core.Enum;
using Almotkaml.ArtificialLimbs.Core.Resources;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Almotkaml.ArtificialLimbs.Models
{
    public class DailyDetailsModel
    {
        public bool CanCreate { get; set; }
        public bool CanEdit { get; set; }
        public bool CanDelete { get; set; }
        public bool CanSubmit { get; set; }
        public string path { get; set; }
        //حالات يومية
        public int DailyDetailsID { get; set; }
        public int DailyStatuesID { get; set; } //رقم الحالة


        [Display(ResourceType = typeof(titles), Name = nameof(titles.PatientName))]
        public int PatientID { get; set; } //رقم المريض

        [Display(ResourceType = typeof(titles), Name = nameof(titles.PatientName))]
        public string PatientName { get; set; }


        [Display(ResourceType = typeof(titles), Name = nameof(titles.Part))]
        public MeasurementModel MeasurementModel { get; set; } //نموذج القياس

        [Display(ResourceType = typeof(titles), Name = nameof(titles.Part))]
        public MeasurementNumberModel Part { get; set; } // (رقم المنطقة(طول/قطر

        [Display(ResourceType = typeof(titles), Name = nameof(titles.AmputationPart))]
        public AmputationPart AmputationPart { get; set; } //جهة البتر


        [Display(ResourceType = typeof(titles), Name = nameof(titles.PartMeasure))]
        public double? PartMeasure { get; set; } //قياس المنطقة

        [Display(ResourceType = typeof(titles), Name = nameof(titles.ShoeNO))]
        public int? ShoeNO { get; set; } // رقم الحذاء

        [Display(ResourceType = typeof(titles), Name = nameof(titles.Note))]
        public string Note { get; set; }
        public IEnumerable<DailyDetailsGridRow> DailyDetailsGrid = new HashSet<DailyDetailsGridRow>();

    }

    public class DailyDetailsGridRow
    {
        //حالات اليومية
        public int DailyDetailsID { get; set; }
        public int DailyStatuesID { get; set; }

        public MeasurementModel MeasurementModel { get; set; }
        public MeasurementNumberModel MeasurementNumberModel { get; set; }
        public AmputationPart AmputationPart { get; set; }
        public double? PartMeasure { get; set; } //قياس المنطقة
        public string Note { get; set; }
        public int? ShoeNO { get; set; }
    }
}