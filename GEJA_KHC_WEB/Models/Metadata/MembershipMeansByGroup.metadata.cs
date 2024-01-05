using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using System.Linq;
using System.Web;

namespace GEJA_KHC_WEB.Models
{
    [MetadataType(typeof(MembershipMeansByGroupMetadata))]
    public partial class MembershipMeansByGroup
    {
    }

    public class MembershipMeansByGroupMetadata
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

        [DisplayName("በጌጃ የልጆች ሰንበት ት/ቤት ያደጉ")]
        public Nullable<int> FromSundaySchool { get; set; }

        [DisplayName("በምስክርነት ደህንነት ተምረው የተጠመቁ ")]
        public Nullable<int> Baptism { get; set; }

        [DisplayName("ከሌላ ቤ/ክ በዝውውር የመጡ")]
        public Nullable<int> Transfer { get; set; }
    }
}