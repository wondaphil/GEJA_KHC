using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace GEJA_KHC_WEB_API.Models.MetaData
{
    [MetadataType(typeof(JobServiceType))]
    public partial class Job
    {
    }

    public partial class JobServiceType
    {
        [Key, Required]
        [DisplayName("መለያ ቁጥር")]
        public int JobId { get; set; }
        
        [DisplayName("የተሰማሩበት ሞያ")]
        public string JobName { get; set; }

        [DisplayName("ማብራሪያ")]
        public string Remark { get; set; }

        [JsonIgnore]
        public virtual ICollection<Member> Member { get; set; }
    }
}
