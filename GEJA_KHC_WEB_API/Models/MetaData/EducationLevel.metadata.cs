using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace GEJA_KHC_WEB_API.Models.MetaData
{
    [MetadataType(typeof(EducationLevelMetadata))]
    public partial class EducationLevel
    {
    }

    public class EducationLevelMetadata
    {
        [Key, Required]
        [DisplayName("ተ.ቁ.")]
        public int EducationLevelId { get; set; }

        [DisplayName("የትምህርት ደረጃ")]
        public string EducationLevelName { get; set; }

        [DisplayName("ማብራሪያ")]
        public string Remark { get; set; }

        [JsonIgnore]
        public virtual ICollection<EducationAndJobInfo> EducationAndJobInfo { get; set; }
    }
}