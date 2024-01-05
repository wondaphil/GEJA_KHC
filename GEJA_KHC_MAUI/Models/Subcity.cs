namespace GEJA_KHC_MAUI.Models
{
    using GEJA_KHC_MAUI.Views;
    using System;
    using System.Collections.Generic;
    
    public partial class Subcity
    {
        public int SubcityId { get; set; }
        public string SubcityName { get; set; }
        public string Remark { get; set; }

        public override string ToString()
        {
            return $"{SubcityName}";
        }
    }
}
