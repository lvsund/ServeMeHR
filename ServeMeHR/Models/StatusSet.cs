//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace ServeMeHR.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class StatusSet
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public StatusSet()
        {
            this.ServiceRequests = new HashSet<ServiceRequest>();
            this.ServiceRequests1 = new HashSet<ServiceRequest>();
        }
    
        public int Id { get; set; }
        public string StatusDescription { get; set; }
        public System.DateTime LastUpdated { get; set; }
        public bool Active { get; set; }
        public int StatusType { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ServiceRequest> ServiceRequests { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ServiceRequest> ServiceRequests1 { get; set; }
        public virtual StatusType StatusType1 { get; set; }
        public virtual StatusType StatusType2 { get; set; }
    }
}
