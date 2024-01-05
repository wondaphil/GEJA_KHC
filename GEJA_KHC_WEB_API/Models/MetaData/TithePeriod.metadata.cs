using GEJA_KHC_WEB_API.Models.MetaData;

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using Newtonsoft.Json;
using System.Web;

namespace GEJA_KHC_WEB_API.Models
{
    [MetadataType(typeof(TithePeriodMetadata))]
    public partial class TithePeriod
    {
    }

    public partial class TithePeriodMetadata
    {
        [Key, Required]
        [DisplayName("ተ.ቁ.")]
        public int TithePeriodId { get; set; }

        [DisplayName("ወር")]
        public string Month { get; set; }

        [DisplayName("ዓመት")]
        public string Year { get; set; }

        [DisplayName("ማብራሪያ")]
        public string Remark { get; set; }

        [JsonIgnore]
        public virtual ICollection<Tithe> Tithes { get; set; }
    }
}
