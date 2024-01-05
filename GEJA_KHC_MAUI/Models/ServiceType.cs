namespace GEJA_KHC_MAUI.Models
{
    using GEJA_KHC_MAUI.Views;
    using System;
    using System.Collections.Generic;
    
    public partial class ServiceType
    {
        public int ServiceTypeId { get; set; }
        public string ServiceTypeName { get; set; }
        public string Remark { get; set; }

        public override string ToString()
        {
            return $"{ServiceTypeName}";
        }
    }
}
