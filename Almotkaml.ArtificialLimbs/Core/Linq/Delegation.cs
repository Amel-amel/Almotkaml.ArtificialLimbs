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
    
    public partial class Delegation
    {
        public int DelegationId { get; set; }
        public System.DateTime DateFrom { get; set; }
        public System.DateTime DateTo { get; set; }
        public string JobNumber { get; set; }
        public int JobTypeTransfer { get; set; }
        public string Name { get; set; }
        public string SideName { get; set; }
        public int C_CreatedBy { get; set; }
        public System.DateTime C_DateCreated { get; set; }
        public System.DateTime C_DateModified { get; set; }
        public int C_ModifiedBy { get; set; }
        public Nullable<System.DateTime> DecisionDate { get; set; }
        public string DelegationNumber { get; set; }
        public Nullable<int> QualificationTypeId { get; set; }
    
        public virtual QualificationType QualificationType { get; set; }
    }
}
