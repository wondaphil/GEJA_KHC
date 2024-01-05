using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using System.Linq;
using System.Web;

namespace GEJA_KHC_WEB.Models
{
    [MetadataType(typeof(ServiceMetadata))]
    public partial class Service
    {
    }

    public class ServiceMetadata
    {
        [Key, Required]
        [DisplayName("የአገልግሎት መለያ ቁጥር")]
        public int ServiceId { get; set; }

        [DisplayName("የአገልግሎት ዘርፍ")]
        public string ServiceName { get; set; }

        [DisplayName("ማብራሪያ")]
        public string Remark { get; set; }
    }
}