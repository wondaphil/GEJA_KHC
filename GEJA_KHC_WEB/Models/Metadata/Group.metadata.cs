using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using System.Linq;
using System.Web;

namespace GEJA_KHC_WEB.Models
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

        [DisplayName("የምድብ ተጠሪ")]
        [Required]
        public string Pastor { get; set; }

        [DisplayName("ማብራሪያ")]
        public string Remark { get; set; }

        [JsonIgnore]
        public virtual ICollection<Member> Member { get; set; }
    }
}