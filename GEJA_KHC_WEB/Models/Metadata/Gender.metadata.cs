using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using System.Linq;
using System.Web;

namespace GEJA_KHC_WEB.Models
{
    [MetadataType(typeof(GenderMetadata))]
    public partial class Gender
    {
    }

    public class GenderMetadata
    {
        [Key, Required]
        [DisplayName("ተ.ቁ.")]
        public int GenderId { get; set; }

        [DisplayName("ፆታ")]
        public string GenderName { get; set; }
    }
}