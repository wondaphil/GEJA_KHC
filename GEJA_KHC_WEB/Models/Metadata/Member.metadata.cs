using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations.Schema;

namespace GEJA_KHC_WEB.Models
{
    [MetadataType(typeof(MemberMetadata))]
    public partial class Member
    {
    }

    public class MemberMetadata
    {
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        [Key, Required]
        [DisplayName("የአባል መለያ ቁጥር")]
        public string MemberId { get; set; }

        [DisplayName("ሙሉ ስም")]
        public string FullName { get; set; }

        [DisplayName("የእናት ስም")]
        public string MotherName { get; set; }

        [DisplayName("ፆታ")]
        public Nullable<int> GenderId { get; set; }

        [DisplayName("የትውልድ ቀን")]
        public Nullable<int> BirthDate { get; set; }

        [DisplayName("የትውልድ ወር")]
        public Nullable<int> BirthMonthId { get; set; }

        [DisplayName("የትውልድ ዘመን")]
        public Nullable<int> BirthYear { get; set; }

        [DisplayName("ምድብ")]
        public Nullable<int> GroupId { get; set; }

        [DisplayName("የአባልነት ቀን")]
        public Nullable<int> MembershipDate { get; set; }

        [DisplayName("የአባልነት ወር")]
        public Nullable<int> MembershipMonthId { get; set; }

        [DisplayName("የአባልነት ዘመን")]
        public Nullable<int> MembershipYear { get; set; }

        [DisplayName("የአባልነት መንገድ")]
        public Nullable<int> MembershipMeansId { get; set; }

        [DisplayName("አገልግሎት ከሌለ ምክንያት")]
        public string NoServiceReason { get; set; }

        //[DisplayName("የአባል ፎቶ")]
        //public string Photo { get; set; }

        [DisplayName("ማብራሪያ")]
        public string Remark { get; set; }
    }
}