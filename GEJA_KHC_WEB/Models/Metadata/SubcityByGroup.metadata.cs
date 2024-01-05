using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using System.Linq;
using System.Web;

namespace GEJA_KHC_WEB.Models
{
    [MetadataType(typeof(SubcityByGroupMetadata))]
    public partial class SubcityByGroup
    {
    }

    public class SubcityByGroupMetadata
    {
        [Key, Required]
        [DisplayName("ተ.ቁ.")]
        public string GroupId { get; set; }
        
        [DisplayName("ምድብ")]
        public string GroupName { get; set; }

        [DisplayName("ጠቅላላ")]
        public Nullable<int> Total { get; set; }

        [DisplayName("ያልታወቀ")]
        public Nullable<int> Unknown { get; set; }

        [DisplayName("ልደታ")]
        public Nullable<int> Lideta { get; set; }

        [DisplayName("አዲስ ከተማ")]
        public Nullable<int> AddisKetema { get; set; }

        [DisplayName("ኮልፌ ቀራንዮ")]
        public Nullable<int> KolfeKeraniyo { get; set; }

        [DisplayName("አራዳ")]
        public Nullable<int> Arada { get; set; }

        [DisplayName("ጉለሌ")]
        public Nullable<int> Gulele { get; set; }

        [DisplayName("ቂርቆስ")]
        public Nullable<int> Kirkos { get; set; }

        [DisplayName("የካ")]
        public Nullable<int> Yeka { get; set; }

        [DisplayName("ቦሌ")]
        public Nullable<int> Bole { get; set; }

        [DisplayName("ንፋስ ስልክ ላፍቶ")]
        public Nullable<int> NifasSilkLafto { get; set; }

        [DisplayName("አቃቂ ቃሊቲ")]
        public Nullable<int> AkakiKality { get; set; }

        [DisplayName("ለሚ ኩራ")]
        public Nullable<int> LemiKura { get; set; }
    }
}