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
    
    public partial class VacationType
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public VacationType()
        {
            this.Vacations = new HashSet<Vacation>();
        }
    
        public int VacationTypeId { get; set; }
        public Nullable<int> Days { get; set; }
        public Nullable<int> DiscountPercentage { get; set; }
        public string Name { get; set; }
        public int C_CreatedBy { get; set; }
        public System.DateTime C_DateCreated { get; set; }
        public System.DateTime C_DateModified { get; set; }
        public int C_ModifiedBy { get; set; }
        public int VacationEssential { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Vacation> Vacations { get; set; }
    }
}
