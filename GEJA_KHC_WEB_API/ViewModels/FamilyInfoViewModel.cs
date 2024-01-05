using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GEJA_KHC_WEB_API.ViewModels
{
    public class FamilyInfoViewModel
    {
        public string MemberId { get; set; }
        public int MaritalStatusId { get; set; }
        public string SpouseName { get; set; }
        public Nullable<int> MarriageYear { get; set; }
        public Nullable<int> NoOfSons { get; set; }
        public Nullable<int> NoOfDaughters { get; set; }
        public Nullable<int> NoOfSons1to5 { get; set; }
        public Nullable<int> NoOfDaughters1to5 { get; set; }
        public Nullable<int> NoOfSons6to12 { get; set; }
        public Nullable<int> NoOfDaughters6to12 { get; set; }
        public Nullable<int> NoOfSons13to20 { get; set; }
        public Nullable<int> NoOfDaughters13to20 { get; set; }
        public Nullable<int> NoOfSonsAbove20 { get; set; }
        public Nullable<int> NoOfDaughtersAbove20 { get; set; }
    }
}