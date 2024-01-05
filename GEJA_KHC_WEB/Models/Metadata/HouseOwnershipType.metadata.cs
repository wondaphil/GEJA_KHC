using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using System.Linq;
using System.Web;
using Newtonsoft.Json;

namespace GEJA_KHC_WEB.Models
{
    [MetadataType(typeof(HouseOwnershipTypeMetadata))]
    public partial class HouseOwnershipType
    {
    }

    public class HouseOwnershipTypeMetadata
    {
        [Key, Required]
        [DisplayName("መለያ ቁጥር")]
        public int HouseOwnershipTypeId { get; set; }

        [DisplayName("የቤት ይዞታ")]
        public string HouseOwnershipTypeName { get; set; }

        [DisplayName("ማብራሪያ")]
        public string Remark { get; set; }
    }
}