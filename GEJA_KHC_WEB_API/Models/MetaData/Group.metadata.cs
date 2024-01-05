using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using Newtonsoft.Json;

namespace GEJA_KHC_WEB_API.Models.MetaData
{
    [MetadataType(typeof(GroupMetadata))]
    public partial class Group
    {
    }

    public class GroupMetadata
    {
        [Key, Required]
        [DisplayName("የምድብ ቁጥር")]
        public int GroupId { get; set; }

        [DisplayName("ምድብ")]
        [Required]
        public string GroupName { get; set; }

        [DisplayName("የምድብ መጋቢ")]
        [Required]
        public string Pastor { get; set; }

        [DisplayName("ማብራሪያ")]
        public int Remark { get; set; }

        [JsonIgnore]
        public virtual ICollection<Member> Member { get; set; }
    }
}