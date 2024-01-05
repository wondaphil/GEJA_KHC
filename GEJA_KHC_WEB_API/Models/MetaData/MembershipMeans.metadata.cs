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
    [MetadataType(typeof(MembershipMeansMetadata))]
    public partial class MembershipMeans
    {
    }

    public partial class MembershipMeansMetadata
    {
        [Key, Required]
        [DisplayName("ተ.ቁ.")]
        public int MembershipMeansId { get; set; }

        [DisplayName("የአባልነት መንገድ")]
        public string MembershipMeansName { get; set; }

        [DisplayName("ማብራሪያ")]
        public string Remark { get; set; }

        [JsonIgnore]
        public virtual ICollection<Member> Member { get; set; }
    }
}
