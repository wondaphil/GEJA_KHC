namespace GEJA_KHC_MAUI.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class HouseOwnershipType
    {
        public int HouseOwnershipTypeId { get; set; }
        public string HouseOwnershipTypeName { get; set; }
        public string Remark { get; set; }

        public override string ToString()
        {
            return $"{HouseOwnershipTypeName}";
        }
    }
}
