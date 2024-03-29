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
    
    public partial class FamilyInfo
    {
        public string MemberId { get; set; }
        public int MaritalStatusId { get; set; }
        public string SpouseName { get; set; }
        public Nullable<int> MarriageYear { get; set; }
        public Nullable<int> NoOfSons { get; set; }
        public Nullable<int> NoOfDaughters { get; set; }
        public Nullable<int> NoOfSons1to5 { get; set; }
        public Nullable<int> NoOfDaughters1to5 { get; set; }
        public Nullable<int> NoOfSons6to12 { get; set; }
        public Nullable<int> NoOfDaughters6to12 { get; set; }
        public Nullable<int> NoOfSons13to20 { get; set; }
        public Nullable<int> NoOfDaughters13to20 { get; set; }
        public Nullable<int> NoOfSonsAbove20 { get; set; }
        public Nullable<int> NoOfDaughtersAbove20 { get; set; }
    
        public virtual MaritalStatus MaritalStatus { get; set; }
        public virtual Member Member { get; set; }
    }
}
