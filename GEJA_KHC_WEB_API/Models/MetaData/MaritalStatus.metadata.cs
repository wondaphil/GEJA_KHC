using GEJA_KHC_WEB_API.Models.MetaData;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace GEJA_KHC_WEB_API.Models
{
    [MetadataType(typeof(MaritalStatusMetadata))]
    public partial class MaritalStatus
    {
    }

    public partial class MaritalStatusMetadata
    {
        [Key, Required]
        [DisplayName("ተ.ቁ.")]
        public int MaritalStatusId { get; set; }

        [DisplayName("የጋብቻ ሁኔታ")]
        public string MaritalStatusName { get; set; }

        [DisplayName("ማብራሪያ")]
        public string Remark { get; set; }
    
        [JsonIgnore]
        public virtual ICollection<FamilyInfo> FamilyInfo { get; set; }
    }
}
