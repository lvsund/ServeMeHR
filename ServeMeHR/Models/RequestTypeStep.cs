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
    
    public partial class RequestTypeStep
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public RequestTypeStep()
        {
            this.ServiceRequests = new HashSet<ServiceRequest>();
            this.ServiceRequests1 = new HashSet<ServiceRequest>();
            this.StepHistories = new HashSet<StepHistory>();
        }
    
        public int Id { get; set; }
        public string StepDescription { get; set; }
        public int StepNumber { get; set; }
        public Nullable<System.DateTime> LastUpdated { get; set; }
        public Nullable<bool> Active { get; set; }
        public int RequestType { get; set; }
    
        public virtual RequestType RequestType1 { get; set; }
        public virtual RequestType RequestType2 { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ServiceRequest> ServiceRequests { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ServiceRequest> ServiceRequests1 { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<StepHistory> StepHistories { get; set; }
    }
}
