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
    
    public partial class HouseOwnershipType
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public HouseOwnershipType()
        {
            this.AddressInfo = new HashSet<AddressInfo>();
        }
    
        public int HouseOwnershipTypeId { get; set; }
        public string HouseOwnershipTypeName { get; set; }
        public string Remark { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<AddressInfo> AddressInfo { get; set; }
    }
}
