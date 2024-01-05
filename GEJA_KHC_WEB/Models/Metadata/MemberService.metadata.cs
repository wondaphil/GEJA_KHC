using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using System.Linq;
using System.Web;

namespace GEJA_KHC_WEB.Models
{
    [MetadataType(typeof(MemberServiceMetadata))]
    public partial class MemberService
    {
    }

    public class MemberServiceMetadata
    {
        [Key, Required]
        [DisplayName("ተ.ቁ.")]
        public int Id { get; set; }

        [DisplayName("የአባል መለያ ቁጥር")]
        public string MemberId { get; set; }

        [DisplayName("የአገልግሎት ዓይነት")]
        public Nullable<int> ServiceTypeId { get; set; }

        [DisplayName("አገልግሎት")]
        public int ServiceId { get; set; }
    }
}