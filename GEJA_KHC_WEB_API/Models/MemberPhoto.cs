//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace GEJA_KHC_WEB_API.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class MemberPhoto
    {
        public string MemberId { get; set; }
        public string PhotoFilePath { get; set; }
        public byte[] Photo { get; set; }
        public string Remark { get; set; }
    
        public virtual Member Member { get; set; }
    }
}
