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
    
    public partial class MembershipMeansByGroup
    {
        public Nullable<long> RowId { get; set; }
        public int GroupId { get; set; }
        public string GroupName { get; set; }
        public Nullable<int> Total { get; set; }
        public Nullable<int> Unknown { get; set; }
        public Nullable<int> FromSundaySchool { get; set; }
        public Nullable<int> Baptism { get; set; }
        public Nullable<int> Transfer { get; set; }
    }
}
