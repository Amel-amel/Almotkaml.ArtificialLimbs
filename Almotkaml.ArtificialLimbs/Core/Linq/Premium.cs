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
    
    public partial class Premium
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Premium()
        {
            this.AdvancePayments = new HashSet<AdvancePayment>();
            this.EmployeePremiums = new HashSet<EmployeePremium>();
            this.SalaryPremiums = new HashSet<SalaryPremium>();
        }
    
        public int PremiumId { get; set; }
        public bool IsSubject { get; set; }
        public string Name { get; set; }
        public int C_CreatedBy { get; set; }
        public System.DateTime C_DateCreated { get; set; }
        public System.DateTime C_DateModified { get; set; }
        public int C_ModifiedBy { get; set; }
        public bool IsTemporary { get; set; }
        public int DiscountOrBoun { get; set; }
        public int ISAdvancePremmium { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<AdvancePayment> AdvancePayments { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<EmployeePremium> EmployeePremiums { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<SalaryPremium> SalaryPremiums { get; set; }
    }
}