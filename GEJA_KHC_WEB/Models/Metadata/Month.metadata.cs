using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using System.Linq;
using System.Web;

namespace GEJA_KHC_WEB.Models
{
    [MetadataType(typeof(MonthMetadata))]
    public partial class Month
    {
    }

    public class MonthMetadata
    {
        [Key, Required]
        [DisplayName("መለያ ቁጥር")]
        public int MonthId { get; set; }

        [DisplayName("ወር")]
        public string MonthName { get; set; }
    }
}