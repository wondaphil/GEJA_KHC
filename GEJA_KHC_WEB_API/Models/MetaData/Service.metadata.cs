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
    [MetadataType(typeof(ServiceMetadata))]
    public partial class Service
    {
    }

    public partial class ServiceMetadata
    {
        [Key, Required]
        [DisplayName("የአገልግሎት መለያ ቁጥር")]
        public int ServiceId { get; set; }

        [DisplayName("አገልግሎት")]
        public string ServiceName { get; set; }
        
        [DisplayName("ማብራሪያ")]
        public string Remark { get; set; }

        [JsonIgnore]
        public virtual ICollection<MemberService> MemberService { get; set; }
    }
}
