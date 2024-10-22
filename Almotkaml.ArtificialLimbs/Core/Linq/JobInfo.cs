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
    
    public partial class JobInfo
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public JobInfo()
        {
            this.EmploymentValues = new HashSet<EmploymentValue>();
        }
    
        public int JobInfoId { get; set; }
        public Nullable<int> AdjectiveEmployeeId { get; set; }
        public Nullable<int> Bouns { get; set; }
        public Nullable<int> CurrentSituationId { get; set; }
        public Nullable<System.DateTime> DateBouns { get; set; }
        public Nullable<System.DateTime> DateDegreeNow { get; set; }
        public Nullable<System.DateTime> DateMeritBouns { get; set; }
        public Nullable<System.DateTime> DateMeritDegreeNow { get; set; }
        public Nullable<int> Degree { get; set; }
        public Nullable<int> DegreeNow { get; set; }
        public Nullable<System.DateTime> DirectlyDate { get; set; }
        public Nullable<int> EmployeeId { get; set; }
        public Nullable<int> JobId { get; set; }
        public int JobNumber { get; set; }
        public Nullable<int> JobNumberApproved { get; set; }
        public int JobType { get; set; }
        public Nullable<int> StaffingId { get; set; }
        public Nullable<int> UnitId { get; set; }
        public Nullable<int> VacationBalance { get; set; }
        public string Notes { get; set; }
        public Nullable<int> ClassificationOnSearchingId { get; set; }
        public Nullable<int> ClassificationOnWorkId { get; set; }
        public Nullable<int> VacationBalanceYear { get; set; }
        public Nullable<int> SalayClassification { get; set; }
        public Nullable<int> JobClass { get; set; }
        public int Redirection { get; set; }
        public string RedirectionNote { get; set; }
        public int Adjective { get; set; }
        public string CurrentClassification { get; set; }
        public string HealthStatus { get; set; }
        public string OldJobNumber { get; set; }
        public string DegreeNote { get; set; }
        public Nullable<int> VacationBalanceAlhaju { get; set; }
        public Nullable<int> VacationBalanceEmergency { get; set; }
        public Nullable<int> VacationBalanceMarriage { get; set; }
        public Nullable<int> VacationBalanceSick { get; set; }
        public Nullable<int> VacationBalanceSickNew { get; set; }
        public Nullable<System.DateTime> DateBounsLast { get; set; }
        public Nullable<int> JobClassValu { get; set; }
        public Nullable<int> Bounshr { get; set; }
        public Nullable<System.DateTime> DateBounshr { get; set; }
        public Nullable<int> SituionsNumber { get; set; }
        public Nullable<int> Situons { get; set; }
    
        public virtual AdjectiveEmployee AdjectiveEmployee { get; set; }
        public virtual ClassificationOnSearching ClassificationOnSearching { get; set; }
        public virtual ClassificationOnWork ClassificationOnWork { get; set; }
        public virtual CurrentSituation CurrentSituation { get; set; }
        public virtual Employee Employee { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<EmploymentValue> EmploymentValues { get; set; }
        public virtual Job Job { get; set; }
        public virtual Staffing Staffing { get; set; }
        public virtual Unit Unit { get; set; }
    }
}
