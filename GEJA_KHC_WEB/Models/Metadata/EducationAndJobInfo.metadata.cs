using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using System.Linq;
using System.Web;

namespace GEJA_KHC_WEB.Models
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
        [Required(ErrorMessage = "* መምረጥ አለብዎ")]
        public Nullable<int> EducationLevelId { get; set; }

        [DisplayName("የትምህርት መስክ")]
        public Nullable<int> FieldOfStudyId { get; set; }

        [DisplayName("የተሰማሩበት የሥራ ዘርፍ")]
        [Required(ErrorMessage = "* መምረጥ አለብዎ")]
        public Nullable<int> JobId { get; set; }

        [DisplayName("የሥራ ቦታ")]
        public Nullable<int> JobTypeId { get; set; }
    }
}