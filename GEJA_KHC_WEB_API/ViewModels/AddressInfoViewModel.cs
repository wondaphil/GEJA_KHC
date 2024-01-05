using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GEJA_KHC_WEB_API.ViewModels
{
    public class AddressInfoViewModel
    {
        public string MemberId { get; set; }
        public Nullable<int> SubcityId { get; set; }
        public string Woreda { get; set; }
        public string Kebele { get; set; }
        public string HouseNo { get; set; }
        public Nullable<int> HouseOwnershipId { get; set; }
        public string HomePhoneNo { get; set; }
        public string OfficePhoneNo { get; set; }
        public string MobilePhoneNo { get; set; }
        public string Email { get; set; }
    }
}