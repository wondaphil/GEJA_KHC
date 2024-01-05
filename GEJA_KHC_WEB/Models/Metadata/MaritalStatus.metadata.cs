using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using System.Linq;
using System.Web;

namespace GEJA_KHC_WEB.Models
{
    [MetadataType(typeof(MaritalStatusMetadata))]
    public partial class MaritalStatus
    {
    }

    public class MaritalStatusMetadata
    {
        [Key, Required]
        [DisplayName("ተ.ቁ.")]
        public int MaritalStatusId { get; set; }

        [DisplayName("የጋብቻ ሁኔታ")]
        public string MaritalStatusName { get; set; }

        [DisplayName("ማብራሪያ")]
        public string Remark { get; set; }
    }
}