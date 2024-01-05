using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GEJA_KHC_WEB_API.ViewModels
{
    public class MemberPhotoViewModel
    {
        public string MemberId { get; set; }
        public string PhotoFilePath { get; set; }
        public byte[] Photo { get; set; }
        public string Remark { get; set; }
    }
}