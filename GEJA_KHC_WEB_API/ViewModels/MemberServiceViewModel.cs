using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GEJA_KHC_WEB_API.ViewModels
{
    public class MemberServiceViewModel
    {
        public int Id { get; set; }
        public string MemberId { get; set; }
        public Nullable<int> ServiceTypeId { get; set; }
        public int ServiceId { get; set; }

    }
}