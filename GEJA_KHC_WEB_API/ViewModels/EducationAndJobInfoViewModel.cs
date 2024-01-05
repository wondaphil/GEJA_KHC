using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GEJA_KHC_WEB_API.ViewModels
{
    public class EducationAndJobInfoViewModel
    {
        public string MemberId { get; set; }
        public Nullable<int> EducationLevelId { get; set; }
        public Nullable<int> FieldOfStudyId { get; set; }
        public Nullable<int> JobId { get; set; }
        public Nullable<int> JobTypeId { get; set; }
    }
}