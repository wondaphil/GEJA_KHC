using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using System.Linq;
using System.Web;

namespace GEJA_KHC_WEB.Models
{
    [MetadataType(typeof(MembershipMeansMetadata))]
    public partial class MembershipMeans
    {
    }

    public class MembershipMeansMetadata
    {
        [Key, Required]
        [DisplayName("ተ.ቁ.")]
        public int MembershipMeansId { get; set; }

        [DisplayName("የአባልነት መንገድ")]
        public string MembershipMeansName { get; set; }

        [DisplayName("ማብራሪያ")]
        public string Remark { get; set; }
    }
}