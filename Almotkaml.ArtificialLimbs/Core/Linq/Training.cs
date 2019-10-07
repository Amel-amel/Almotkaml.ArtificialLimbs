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
    
    public partial class Training
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Training()
        {
            this.TrainingDetails = new HashSet<TrainingDetail>();
        }
    
        public int TrainingId { get; set; }
        public int CityId { get; set; }
        public System.DateTime DateFrom { get; set; }
        public System.DateTime DateTo { get; set; }
        public string DecisionNumber { get; set; }
        public int DeducationType { get; set; }
        public Nullable<int> DevelopmentStateId { get; set; }
        public string ExecutingAgency { get; set; }
        public Nullable<int> RequestedQualificationId { get; set; }
        public string Subject { get; set; }
        public int DevelopmentTypeDId { get; set; }
        public int C_CreatedBy { get; set; }
        public System.DateTime C_DateCreated { get; set; }
        public System.DateTime C_DateModified { get; set; }
        public int C_ModifiedBy { get; set; }
        public int TrainingPlace { get; set; }
        public Nullable<int> CorporationId { get; set; }
        public Nullable<int> CourseId { get; set; }
        public System.DateTime DecisionDate { get; set; }
        public int TrainingType { get; set; }
    
        public virtual City City { get; set; }
        public virtual Corporation Corporation { get; set; }
        public virtual Cours Cours { get; set; }
        public virtual DevelopmentState DevelopmentState { get; set; }
        public virtual DevelopmentTypeD DevelopmentTypeD { get; set; }
        public virtual RequestedQualification RequestedQualification { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<TrainingDetail> TrainingDetails { get; set; }
    }
}
