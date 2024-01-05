using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace GEJA_KHC_WEB.Models
{
    [MetadataType(typeof(AspNetUsersMetadata))]
    public partial class AspNetUsers
    {
    }

    public class AspNetUsersMetadata
    {
        [Key]
        [DisplayName("የተጠቃሚ መለያ ቁ.")]
        public string Id { get; set; }

        [DisplayName("የመጠቃሚያ ስም")]
        public string UserName { get; set; }

        [DisplayName("ኢሚይል")]
        public string Email { get; set; }
    } 
}