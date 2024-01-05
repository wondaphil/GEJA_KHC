using GEJA_KHC_WEB_API.Models.MetaData;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace GEJA_KHC_WEB_API.Models.MetaData
{
    [MetadataType(typeof(MemberMetadata))]
    public partial class Member
    {
    }

    public partial class MemberMetadata
    {
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
        public int GroupId { get; set; }

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
        
        [DisplayName("ማብራሪያ")]
        public string Remark { get; set; }

        
        [JsonIgnore]
        public virtual AddressInfo AddressInfo { get; set; }

        [JsonIgnore]
        public virtual EducationAndJobInfo EducationAndJobInfo { get; set; }

        [JsonIgnore]
        public virtual FamilyInfo FamilyInfo { get; set; }

        [JsonIgnore]
        public virtual Group Group { get; set; }

        [JsonIgnore]
        public virtual ICollection<MemberService> MemberService { get; set; }

        [JsonIgnore]
        public virtual MemberPhoto MemberPhoto { get; set; }

        [JsonIgnore]
        public virtual MembershipMeans MembershipMeans { get; set; }

        [JsonIgnore]
        public virtual Month Month { get; set; }

        [JsonIgnore]
        public virtual Month Month1 { get; set; }
    }
}
