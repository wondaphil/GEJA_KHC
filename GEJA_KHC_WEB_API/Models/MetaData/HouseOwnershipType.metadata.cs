using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace GEJA_KHC_WEB_API.Models.MetaData
{
    [MetadataType(typeof(HouseOwnershipTypeMetadata))]
    public partial class HouseOwnershipType
    {
    }

    public partial class HouseOwnershipTypeMetadata
    {
        [Key, Required]
        [DisplayName("መለያ ቁጥር")]
        public int HouseOwnershipTypeId { get; set; }
        
        [DisplayName("የቤት ይዞታ")]
        public string HouseOwnershipTypeName { get; set; }

        [DisplayName("ማብራሪያ")]
        public string Remark { get; set; }
        [JsonIgnore]
        public virtual ICollection<Member> Member { get; set; }
    }
}
