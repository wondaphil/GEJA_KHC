using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using System.Linq;
using System.Web;

namespace GEJA_KHC_WEB.Models
{
    [MetadataType(typeof(EducationLevelByGroupMetadata))]
    public partial class EducationLevelByGroup
    {
    }

    public class EducationLevelByGroupMetadata
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

        [DisplayName("12 ያላጠናቀቁ")]
        public Nullable<int> TwelveIncomplete { get; set; }

        [DisplayName("12 ያጠናቀቁ")]
        public Nullable<int> TwelveComplete { get; set; }

        [DisplayName("ሠርተፊኬት")]
        public Nullable<int> Certificate { get; set; }

        [DisplayName("ዲፕሎማ")]
        public Nullable<int> Diploma { get; set; }

        [DisplayName("የመጀመሪያ ዲግሪ")]
        public Nullable<int> FirstDegree { get; set; }

        [DisplayName("2ኛ ዲግሪ")]
        public Nullable<int> SecondDegree { get; set; }

        [DisplayName("3ኛ ዲግሪ")]
        public Nullable<int> ThirdDegree { get; set; }
    }
}