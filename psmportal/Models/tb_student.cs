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
    
    public partial class tb_student
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public tb_student()
        {
            this.tb_request = new HashSet<tb_request>();
        }
    
        public string IC { get; set; }
        public string MatricNo { get; set; }
        public string Name { get; set; }
        public string ProgramCode { get; set; }
        public string Email { get; set; }
        public string MobileNo { get; set; }
        public string AcademicSession { get; set; }
        public string Supervisor { get; set; }
        public byte[] Agreement { get; set; }
        public Nullable<int> Domain { get; set; }
        public string ProposalID { get; set; }
    
        public virtual tb_domain tb_domain { get; set; }
        public virtual tb_program tb_program { get; set; }
        public virtual tb_proposal tb_proposal { get; set; }
        public virtual tb_sv tb_sv { get; set; }
        public virtual tb_user tb_user { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<tb_request> tb_request { get; set; }
    }
}
