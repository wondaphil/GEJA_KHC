using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GEJA_KHC_WEB_API.ViewModels
{
    public class GroupViewModel
    {
        public int GroupId { get; set; }
        public string GroupName { get; set; }
        public string Pastor { get; set; }
        public string Remark { get; set; }
    }
}