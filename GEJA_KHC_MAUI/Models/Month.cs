namespace GEJA_KHC_MAUI.Models
{
    using GEJA_KHC_MAUI.Views;
    using System;
    using System.Collections.Generic;
    
    public partial class Month
    {
        public int MonthId { get; set; }
        public string MonthName { get; set; }

        public override string ToString()
        {
            return $"{MonthName}";
        }
    }
}
