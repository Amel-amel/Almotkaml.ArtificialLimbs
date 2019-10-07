using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Almotkaml.ArtificialLimbs.Models
{
    public class HomeModel
    {
        public int PatientCount { get; set; }//عدد المرضى
        public int NewPatientCount { get; set; }//حالات الانتظار
        public int AmpPatientCount { get; set; }//حالات البتر
        public int MenCount { get; set; }//عدد الرجال
        public int WomenCount { get; set; }//نساء
        public int ChildrenCount { get; set; }//أطفال
    }
   
}