using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using System.Linq;
using System.Web;

namespace GEJA_KHC_WEB.Models
{
    [MetadataType(typeof(JobTypeMetadata))]
    public partial class JobType
    {
    }

    public class JobTypeMetadata
    {
        [Key, Required]
        [DisplayName("ተ.ቁ.")]
        public int JobTypeId { get; set; }

        [DisplayName("የሥራ ቦታ")]
        public string JobTypeName { get; set; }

        [DisplayName("ማብራሪያ")]
        public string Remark { get; set; }
    }
}