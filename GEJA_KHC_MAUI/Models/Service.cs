namespace GEJA_KHC_MAUI.Models
{
    using GEJA_KHC_MAUI.Views;
    using System;
    using System.Collections.Generic;
    
    public partial class Service
    {
        public int ServiceId { get; set; }
        public string ServiceName { get; set; }
        public string Remark { get; set; }

        public override string ToString()
        {
            return $"{ServiceName}";
        }
    }
}
