﻿using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace GEJA_KHC_WEB_API.Models.MetaData
{
    [MetadataType(typeof(AddressInfoMetadata))]
    public partial class AddressInfo
    {
    }

    public class AddressInfoMetadata
    {
        [Key, Required]
        [DisplayName("የአባል መለያ ቁጥር")]
        public string MemberId { get; set; }

        [DisplayName("ክ/ከተማ")]
        public Nullable<int> SubcityId { get; set; }

        [DisplayName("ወረዳ")]
        public string Woreda { get; set; }

        [DisplayName("ቀበሌ")]
        public string Kebele { get; set; }

        [DisplayName("የቤት ቁጥር")]
        public string HouseNo { get; set; }

        [DisplayName("የቤቱ ይዞታ")]
        public Nullable<int> HouseOwnershipId { get; set; }

        [DisplayName("የቤት ስልክ")]
        public string HomePhoneNo { get; set; }

        [DisplayName("የቢሮ ስልክ")]
        public string OfficePhoneNo { get; set; }

        [DisplayName("የሞባይል ስልክ")]
        public string MobilePhoneNo { get; set; }

        [DisplayName("ኢሜይል")]
        public string Email { get; set; }


        [JsonIgnore]
        public virtual Member Member { get; set; }
        
        [JsonIgnore]
        public virtual HouseOwnershipType HouseOwnershipType { get; set; }
        
        [JsonIgnore]
        public virtual Subcity Subcity { get; set; }
    }
}