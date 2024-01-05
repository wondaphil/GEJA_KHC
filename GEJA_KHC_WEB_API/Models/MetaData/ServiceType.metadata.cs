using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace GEJA_KHC_WEB_API.Models.MetaData
{
    [MetadataType(typeof(ServiceTypeMetadata))]
    public partial class ServiceType
    {
    }

    public partial class ServiceTypeMetadata
    {
        [Key, Required]
        [DisplayName("መለያ ቁጥር")]
        public int ServiceTypeId { get; set; }

        [DisplayName("የአገልግሎት ዘርፍ")]
        public string ServiceTypeName { get; set; }

        [JsonIgnore]
        public virtual ICollection<MemberService> MemberService { get; set; }
    }
}
