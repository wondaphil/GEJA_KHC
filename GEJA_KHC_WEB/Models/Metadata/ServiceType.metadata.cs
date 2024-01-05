using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using System.Linq;
using System.Web;

namespace GEJA_KHC_WEB.Models
{
    [MetadataType(typeof(ServiceTypeMetadata))]
    public partial class ServiceType
    {
    }

    public class ServiceTypeMetadata
    {
        [Key, Required]
        [DisplayName("መለያ ቁጥር")]
        public int ServiceTypeId { get; set; }

        [DisplayName("የአገልግሎት ዓይነት")]
        public string ServiceTypeName { get; set; }
    }
}