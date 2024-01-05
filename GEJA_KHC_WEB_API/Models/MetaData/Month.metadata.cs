using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace GEJA_KHC_WEB_API.Models.MetaData
{
    [MetadataType(typeof(MonthMetadata))]
    public partial class Month
    {
    }

    public partial class MonthMetadata
    {
        [Key, Required]
        [DisplayName("መለያ ቁጥር")]
        public int MonthId { get; set; }

        [DisplayName("ወር")]
        public string MonthName { get; set; }
    
        [JsonIgnore]
        public virtual ICollection<Member> Member { get; set; }
        
        [JsonIgnore]
        public virtual ICollection<Member> Member1 { get; set; }
    }
}
