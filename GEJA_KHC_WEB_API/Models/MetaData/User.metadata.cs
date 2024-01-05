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
    [MetadataType(typeof(UserMetadata))]
    public partial class User
    {
    }

    public partial class UserMetadata
    {
        [Key, Required]
        [DisplayName("የተጠቃሚ መለያ ቁጥር")]
        public string UserId { get; set; }

        [DisplayName("የተጠቃሚ መለያ ስም")]
        public string UserName { get; set; }

        [DisplayName("የተጠቃሚ የሚስጥር ቁጥር")]
        public string Password { get; set; }

        [DisplayName("ኢሚይል")]
        public string Email { get; set; }

        [DisplayName("የተጠቃሚ ልዩ መብት")]
        public int Privilege { get; set; }
    }
}
