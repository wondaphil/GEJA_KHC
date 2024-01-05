using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GEJA_KHC_WEB_API.ViewModels
{
    public class FieldOfStudyViewModel
    {
        public int FieldOfStudyId { get; set; }
        public string FieldOfStudyName { get; set; }
        public string EnglishName { get; set; }
        public string Remark { get; set; }
    }
}