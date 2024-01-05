using GEJA_KHC_WEB_API.Models.MetaData;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace GEJA_KHC_WEB_API.Models
{
    [MetadataType(typeof(TitheMetadata))]
    public partial class Tithe
    {
    }

    public partial class TitheMetadata
    {
        [Key, Required]
        [DisplayName("ተ.ቁ.")]
        public int TitheId { get; set; }

        [DisplayName("የአባል መለያ ቁጥር")]
        public string MemberId { get; set; }

        [DisplayName("የአስራት ክፍያ ጊዜ")]
        public int TithePeriodId { get; set; }

        [DisplayName("የክፍያ መጠን")]
        public double Amount { get; set; }

        [DisplayName("ማብራሪያ")]
        public string Remark { get; set; }

        [JsonIgnore]
        public virtual Member Member { get; set; }

        [JsonIgnore]
        public virtual TithePeriod TithePeriod { get; set; }
    }
}
