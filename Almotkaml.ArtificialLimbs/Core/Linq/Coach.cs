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
    
    public partial class Coach
    {
        public int CoachId { get; set; }
        public int CoachType { get; set; }
        public string Email { get; set; }
        public Nullable<int> EmployeeId { get; set; }
        public string Name { get; set; }
        public string Note { get; set; }
        public string Phone { get; set; }
        public int C_CreatedBy { get; set; }
        public System.DateTime C_DateCreated { get; set; }
        public System.DateTime C_DateModified { get; set; }
        public int C_ModifiedBy { get; set; }
    
        public virtual Employee Employee { get; set; }
    }
}
