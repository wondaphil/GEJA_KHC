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
    [MetadataType(typeof(AspNetRolesMetadata))]
    public partial class AspNetRoles
    {
    }

    public class AspNetRolesMetadata
    {
        [Key]
        [DisplayName("የተጠቃሚ ደረጃ ቁ.")]
        public string Id { get; set; }

        [DisplayName("የተጠቃሚ ደረጃ")]
        public string Name { get; set; }
    } 
}