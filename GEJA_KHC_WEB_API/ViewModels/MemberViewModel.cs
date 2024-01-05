using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GEJA_KHC_WEB_API.ViewModels
{
    public class MemberViewModel
    {
        public string MemberId { get; set; }
        public string FullName { get; set; }
        public string MotherName { get; set; }
        public Nullable<int> GenderId { get; set; }
        public Nullable<int> BirthDate { get; set; }
        public Nullable<int> BirthMonthId { get; set; }
        public Nullable<int> BirthYear { get; set; }
        public Nullable<int> GroupId { get; set; }
        public Nullable<int> MembershipDate { get; set; }
        public Nullable<int> MembershipMonthId { get; set; }
        public Nullable<int> MembershipYear { get; set; }
        public Nullable<int> MembershipMeansId { get; set; }
        public string NoServiceReason { get; set; }
        public string Remark { get; set; }
    }
}