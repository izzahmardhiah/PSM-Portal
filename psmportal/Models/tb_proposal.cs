//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace psmportal.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class tb_proposal
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public tb_proposal()
        {
            this.tb_student = new HashSet<tb_student>();
            this.tb_evaluation = new HashSet<tb_evaluation>();
        }
    
        public string ProposalID { get; set; }
        public string Title { get; set; }
        public string ProposalDoc { get; set; }
        public Nullable<System.DateTime> DateUploaded { get; set; }
        public string Notes { get; set; }
        public string Evaluator1 { get; set; }
        public string Evaluator2 { get; set; }
        public string StudentIC { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<tb_student> tb_student { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<tb_evaluation> tb_evaluation { get; set; }
    }
}
