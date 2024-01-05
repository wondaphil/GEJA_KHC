using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using System.Linq;
using System.Web;

namespace GEJA_KHC_WEB.Models
{
    [MetadataType(typeof(FieldOfStudyMetadata))]
    public partial class FieldOfStudy
    {
    }

    public class FieldOfStudyMetadata
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
    }
}