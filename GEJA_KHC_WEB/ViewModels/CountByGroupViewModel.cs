using GEJA_KHC_WEB.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GEJA_KHC_WEB.ViewModels
{
    public class CountByGroupViewModel
    {
        public Group Groups { get; set; }
        public int MemberCount { get; set; }
    }
}