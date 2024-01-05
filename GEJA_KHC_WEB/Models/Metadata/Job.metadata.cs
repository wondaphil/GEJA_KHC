using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using System.Linq;
using System.Web;

namespace GEJA_KHC_WEB.Models
{
    [MetadataType(typeof(JobMetadata))]
    public partial class Job
    {
    }

    public class JobMetadata
    {
        [Key, Required]
        [DisplayName("መለያ ቁጥር")]
        public int JobId { get; set; }

        [DisplayName("የተሰማሩበት ሞያ")]
        public string JobName { get; set; }

        [DisplayName("ማብራሪያ")]
        public string Remark { get; set; }
    }
}