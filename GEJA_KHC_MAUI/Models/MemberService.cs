//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace GEJA_KHC_MAUI.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class MemberService
    {
        public int Id { get; set; }
        public string MemberId { get; set; }
        public Nullable<int> ServiceTypeId { get; set; }
        public int ServiceId { get; set; }
    
        public virtual Service Service { get; set; }
        public virtual ServiceType ServiceType { get; set; }
        public virtual Member Member { get; set; }
    }
}
