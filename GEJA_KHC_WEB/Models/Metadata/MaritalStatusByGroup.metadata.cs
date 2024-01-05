using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using System.Linq;
using System.Web;

namespace GEJA_KHC_WEB.Models
{
    [MetadataType(typeof(MaritalStatusByGroupMetadata))]
    public partial class MaritalStatusByGroup
    {
    }

    public class MaritalStatusByGroupMetadata
    {
        [Key, Required]
        [DisplayName("ተ.ቁ.")]
        public string GroupId { get; set; }
        
        [DisplayName("ምድብ")]
        public string GroupName { get; set; }

        [DisplayName("ጠቅላላ")]
        public Nullable<int> Total { get; set; }

        [DisplayName("ያልታወቀ")]
        public Nullable<int> Unknown { get; set; }

        [DisplayName("ያላገባ/ች")]
        public Nullable<int> Single { get; set; }

        [DisplayName("ያገባ/ች")]
        public Nullable<int> Married { get; set; }

        [DisplayName("የተፋታ/ች")]
        public Nullable<int> Divorced { get; set; }

        [DisplayName("በሞት የተለየ/ች")]
        public Nullable<int> Widowed { get; set; }
    }
}