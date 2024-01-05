using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace GEJA_KHC_WEB_API.Models.MetaData
{
    [MetadataType(typeof(MemberServiceMetadata))]
    public partial class MemberService
    {
    }

    public partial class MemberServiceMetadata
    {
        [Key, Required]
        [DisplayName("ተ.ቁ.")]
        public int Id { get; set; }
        
        [DisplayName("የአባል መለያ ቁጥር")]
        public string MemberId { get; set; }

        [DisplayName("የአገልግሎት ዓይነት")]
        public Nullable<int> ServiceTypeId { get; set; }

        public int ServiceId { get; set; }

        [JsonIgnore]
        public virtual Member Member { get; set; }

        [JsonIgnore]
        public virtual Service Service { get; set; }

        [JsonIgnore]
        public virtual ServiceType ServiceType { get; set; }
    }
}
