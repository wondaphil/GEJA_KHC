using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace GEJA_KHC_WEB_API.Models.MetaData
{
    [MetadataType(typeof(FieldOfStudyMetadata))]
    public partial class FieldOfStudy
    {
    }
    
    public partial class FieldOfStudyMetadata
    {
        [Key, Required]
        [DisplayName("መለያ ቁጥር")]
        public int FieldOfStudyId { get; set; }
        
        [DisplayName("የትምህርት መስክ")]
        public string FieldOfStudyName { get; set; }

        [DisplayName("የትምህርት መስክ (በእንግሊዝኛ)")]
        public string EnglishName { get; set; }

        [DisplayName("ማብራሪያ")]
        public string Remark { get; set; }

        [JsonIgnore]
        public virtual ICollection<Member> Member { get; set; }
    }
}
