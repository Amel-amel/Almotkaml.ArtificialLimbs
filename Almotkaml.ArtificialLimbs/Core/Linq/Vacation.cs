//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Almotkaml.ArtificialLimbs.Core.Linq
{
    using System;
    using System.Collections.Generic;
    
    public partial class Vacation
    {
        public long VacationId { get; set; }
        public System.DateTime DateFrom { get; set; }
        public System.DateTime DateTo { get; set; }
        public Nullable<System.DateTime> DecisionDate { get; set; }
        public string DecisionNumber { get; set; }
        public int EmployeeId { get; set; }
        public bool Place { get; set; }
        public int VacationTypeId { get; set; }
        public int C_CreatedBy { get; set; }
        public System.DateTime C_DateCreated { get; set; }
        public System.DateTime C_DateModified { get; set; }
        public int C_ModifiedBy { get; set; }
        public string Note { get; set; }
        public Nullable<int> CountKids { get; set; }
    
        public virtual Employee Employee { get; set; }
        public virtual VacationType VacationType { get; set; }
    }
}
