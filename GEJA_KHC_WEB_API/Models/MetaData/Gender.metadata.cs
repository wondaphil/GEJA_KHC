using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using Newtonsoft.Json;

namespace GEJA_KHC_WEB_API.Models.MetaData
{
    [MetadataType(typeof(GenderMetadata))]
    public partial class Gender
    {
    }

    public class GenderMetadata
    {
        [Key, Required]
        [DisplayName("ተ.ቁ.")]
        public int GenderId { get; set; }

        [DisplayName("ፆታ")]
        public string GenderName { get; set; }

        [JsonIgnore]
        public virtual ICollection<Member> Member { get; set; }
    }
}