using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using System.Linq;
using System.Web;

namespace GEJA_KHC_WEB.Models
{
    [MetadataType(typeof(GenderByGroupMetadata))]
    public partial class GenderByGroup
    {
    }

    public class GenderByGroupMetadata
    {
        [Key, Required]
        [DisplayName("ተ.ቁ.")]
        public int GroupId { get; set; }
        
        [DisplayName("ምድብ")]
        public string GroupName { get; set; }

        [DisplayName("ጠቅላላ")]
        public Nullable<int> Total { get; set; }

        [DisplayName("ወንድ")]
        public Nullable<int> Male { get; set; }

        [DisplayName("ሴት")]
        public Nullable<int> Female { get; set; }
    }
}