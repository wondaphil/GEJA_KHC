using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using System.Linq;
using System.Web;

namespace GEJA_KHC_WEB.Models
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
    }
}