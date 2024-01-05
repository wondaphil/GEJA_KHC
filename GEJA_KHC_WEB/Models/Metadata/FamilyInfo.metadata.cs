using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using System.Linq;
using System.Web;

namespace GEJA_KHC_WEB.Models
{
    [MetadataType(typeof(FamilyInfoMetadata))]
    public partial class FamilyInfo
    {
    }

    public class FamilyInfoMetadata
    {
        [Key, Required]
        [DisplayName("የአባል መለያ ቁጥር")]
        public string MemberId { get; set; }

        [DisplayName("የጋብቻ ሁኔታ")]
        [Required(ErrorMessage = "* መምረጥ አለብዎ")]
        public int MaritalStatusId { get; set; }

        [DisplayName("የትዳር አጋር")]
        public string SpouseName { get; set; }

        [DisplayName("የጋብቻ ዘመን")]
        public Nullable<int> MarriageYear { get; set; }

        [DisplayName("የወንድ ልጆች ብዛት")]
        public Nullable<int> NoOfSons { get; set; }

        [DisplayName("የሴት ልጆች ብዛት")]
        public Nullable<int> NoOfDaughters { get; set; }

        [DisplayName("ዕድሜ 1-5")]
        public Nullable<int> NoOfSons1to5 { get; set; }

        [DisplayName("ዕድሜ 1-5")]
        public Nullable<int> NoOfDaughters1to5 { get; set; }

        [DisplayName("ዕድሜ 6-12")]
        public Nullable<int> NoOfSons6to12 { get; set; }

        [DisplayName("ዕድሜ 6-12")]
        public Nullable<int> NoOfDaughters6to12 { get; set; }

        [DisplayName("ዕድሜ 13-20")]
        public Nullable<int> NoOfSons13to20 { get; set; }

        [DisplayName("ዕድሜ 13-20")]
        public Nullable<int> NoOfDaughters13to20 { get; set; }

        [DisplayName("ዕድሜ ከ20 በላይ")]
        public Nullable<int> NoOfSonsAbove20 { get; set; }

        [DisplayName("ዕድሜ ከ20 በላይ")]
        public Nullable<int> NoOfDaughtersAbove20 { get; set; }
    }
}