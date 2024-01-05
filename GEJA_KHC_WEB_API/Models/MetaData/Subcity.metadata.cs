using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace GEJA_KHC_WEB_API.Models
{
    [MetadataType(typeof(SubcityMetadata))]
    public partial class Subcity
    {
    }

    public partial class SubcityMetadata
    {
        [Key, Required]
        [DisplayName("ተ.ቁ.")]
        public int SubcityId { get; set; }

        [DisplayName("ክ/ከተማ")]
        public string SubcityName { get; set; }

        [DisplayName("ማብራሪያ")]
        public string Remark { get; set; }

        [JsonIgnore]
        public virtual ICollection<AddressInfo> AddressInfo { get; set; }
    }
}
