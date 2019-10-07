using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Almotkaml.ArtificialLimbs.Core.Domain
{
    public class PatientReport
    {
        public int PatientReportID { get; set; }
        public int PatientID { get; set; }//رقم المريض
        public Patient patient { get; set; }//جدول المريض
        public int Description { get; set; }//وصف
        public byte[] Image { get; set; }// صورة التقرير
    }
}