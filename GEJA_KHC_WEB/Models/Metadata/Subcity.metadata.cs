using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using System.Linq;
using System.Web;

namespace GEJA_KHC_WEB.Models
{
    [MetadataType(typeof(SubcityMetadata))]
    public partial class Subcity
    {
    }

    public class SubcityMetadata
    {
        [Key, Required]
        [DisplayName("ተ.ቁ.")]
        public int SubcityId { get; set; }

        [DisplayName("ክ/ከተማ")]
        public string SubcityName { get; set; }

        [DisplayName("ማብራሪያ")]
        public string Remark { get; set; }
    }
}