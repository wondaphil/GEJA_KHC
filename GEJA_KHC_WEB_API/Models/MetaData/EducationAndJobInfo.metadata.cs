using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace GEJA_KHC_WEB_API.Models.MetaData
{
    [MetadataType(typeof(EducationAndJobInfoMetadata))]
    public partial class EducationAndJobInfo
    {
    }

    public class EducationAndJobInfoMetadata
    {
        [Key, Required]
        [DisplayName("የአባል መለያ ቁጥር")]
        public string MemberId { get; set; }
        
        [DisplayName("የትምህርት ደረጃ")]
        public Nullable<int> EducationLevelId { get; set; }

        [DisplayName("የትምህርት መስክ")]
        public Nullable<int> FieldOfStudyId { get; set; }

        [DisplayName("የተሰማሩበት የሥራ ዘርፍ")]
        public Nullable<int> JobId { get; set; }

        [DisplayName("የሥራ ቦታ")]
        public Nullable<int> JobTypeId { get; set; }

        
        [JsonIgnore]
        public virtual EducationLevel EducationLevel { get; set; }
        
        [JsonIgnore]
        public virtual FieldOfStudy FieldOfStudy { get; set; }

        [JsonIgnore]
        public virtual Job Job { get; set; }
        
        [JsonIgnore]
        public virtual JobType JobType { get; set; }
        
        [JsonIgnore]
        public virtual Member Member { get; set; }
    }
}