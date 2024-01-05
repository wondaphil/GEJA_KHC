namespace GEJA_KHC_MAUI.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class MemberPhoto
    {
        public string MemberId { get; set; }
        public string PhotoFilePath { get; set; }
        public byte[] Photo { get; set; }
        public string Remark { get; set; }

        public override string ToString()
        {
            return $"{PhotoFilePath}";
        }
    }
}
