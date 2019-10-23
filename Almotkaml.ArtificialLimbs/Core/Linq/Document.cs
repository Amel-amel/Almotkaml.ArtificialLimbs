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
    
    public partial class Document
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Document()
        {
            this.DocumentImages = new HashSet<DocumentImage>();
        }
    
        public int DocumentId { get; set; }
        public Nullable<int> DecisionNumber { get; set; }
        public Nullable<int> DecisionYear { get; set; }
        public int DocumentTypeId { get; set; }
        public int EmployeeId { get; set; }
        public Nullable<System.DateTime> ExpireDate { get; set; }
        public System.DateTime IssueDate { get; set; }
        public string IssuePlace { get; set; }
        public string Note { get; set; }
        public int Number { get; set; }
        public int C_CreatedBy { get; set; }
        public System.DateTime C_DateCreated { get; set; }
        public System.DateTime C_DateModified { get; set; }
        public int C_ModifiedBy { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<DocumentImage> DocumentImages { get; set; }
        public virtual DocumentType DocumentType { get; set; }
        public virtual Employee Employee { get; set; }
    }
}