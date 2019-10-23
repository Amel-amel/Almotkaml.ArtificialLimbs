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
    
    public partial class Salary
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Salary()
        {
            this.SalaryPremiums = new HashSet<SalaryPremium>();
            this.TemporaryPremiums = new HashSet<TemporaryPremium>();
        }
    
        public long SalaryId { get; set; }
        public int AbsenceDays { get; set; }
        public decimal WithoutPay { get; set; }
        public int BankBranchId { get; set; }
        public decimal BasicSalary { get; set; }
        public string BondNumber { get; set; }
        public System.DateTime Date { get; set; }
        public int EmployeeId { get; set; }
        public decimal ExtraWorkHoures { get; set; }
        public decimal ExtraWorkVacationHoures { get; set; }
        public string JobNumber { get; set; }
        public int Month { get; set; }
        public decimal PrepaidSalary { get; set; }
        public decimal Sanction { get; set; }
        public int C_CreatedBy { get; set; }
        public System.DateTime C_DateCreated { get; set; }
        public System.DateTime C_DateModified { get; set; }
        public int C_ModifiedBy { get; set; }
        public bool IsClose { get; set; }
        public System.DateTime MonthDate { get; set; }
        public bool IsSuspended { get; set; }
        public string SocialSecurityFundBondNumber { get; set; }
        public string SolidarityFundBondNumber { get; set; }
        public string TaxBondNumber { get; set; }
        public decimal ExtraValue { get; set; }
        public decimal AdvancePremiumInside { get; set; }
        public decimal AdvancePremiumOutside { get; set; }
        public string SuspendedNote { get; set; }
        public decimal ExtraGeneralValue { get; set; }
        public string FileNumber { get; set; }
        public decimal DifrancessSetting { get; set; }
        public bool IsSubsendedSalary { get; set; }
    
        public virtual BankBranch BankBranch { get; set; }
        public virtual Employee Employee { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<SalaryPremium> SalaryPremiums { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<TemporaryPremium> TemporaryPremiums { get; set; }
    }
}