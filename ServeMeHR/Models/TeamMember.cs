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
    
    public partial class TeamMember
    {
        public int Id { get; set; }
        public int Member { get; set; }
        public int Team { get; set; }
    
        public virtual Member Member1 { get; set; }
        public virtual Team Team1 { get; set; }
    }
}