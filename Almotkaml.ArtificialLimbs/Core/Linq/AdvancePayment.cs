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
    
    public partial class AdvancePayment
    {
        public int AdvancePaymentId { get; set; }
        public System.DateTime DeductionDate { get; set; }
        public int EmployeeId { get; set; }
        public decimal InstallmentValue { get; set; }
        public decimal Value { get; set; }
        public int C_CreatedBy { get; set; }
        public System.DateTime C_DateCreated { get; set; }
        public System.DateTime C_DateModified { get; set; }
        public int C_ModifiedBy { get; set; }
        public bool IsInside { get; set; }
        public System.DateTime Date { get; set; }
        public int PremiumId { get; set; }
        public decimal AllValue { get; set; }
    
        public virtual Employee Employee { get; set; }
        public virtual Premium Premium { get; set; }
    }
}
