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
    
    public partial class AddressInfo
    {
        public string MemberId { get; set; }
        public Nullable<int> SubcityId { get; set; }
        public string Woreda { get; set; }
        public string Kebele { get; set; }
        public string HouseNo { get; set; }
        public Nullable<int> HouseOwnershipId { get; set; }
        public string HomePhoneNo { get; set; }
        public string OfficePhoneNo { get; set; }
        public string MobilePhoneNo { get; set; }
        public string Email { get; set; }
    
        public virtual HouseOwnershipType HouseOwnershipType { get; set; }
        public virtual Subcity Subcity { get; set; }
        public virtual Member Member { get; set; }
    }
}