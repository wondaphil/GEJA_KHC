using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace GEJA_KHC_WEB_API.Models.MetaData
{
    [MetadataType(typeof(JobTypeServiceType))]
    public partial class JobType
    {
    }

    public partial class JobTypeServiceType
    {
        [Key, Required]
        [DisplayName("ተ.ቁ.")]
        public int JobTypeId { get; set; }

        [DisplayName("የሥራ ቦታ")]
        public string JobTypeName { get; set; }

        [DisplayName("ማብራሪያ")]
        public string Remark { get; set; }

        [JsonIgnore]
        public virtual ICollection<Member> Member { get; set; }
    }
}
