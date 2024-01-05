using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using GEJA_KHC_WEB.Models;

namespace GEJA_KHC_WEB.ViewModels
{
    public class MemberViewModel
    {
        public Member MemberInfo { get; set; }
        public MemberPhoto MemberPhoto { get; set; }
        public AddressInfo AddressInfo { get; set; }
        public FamilyInfo FamilyInfo { get; set; }
        public EducationAndJobInfo EducationAndJobInfo { get; set; }
        public List<MemberService> MemberService { get; set; }
    }
}