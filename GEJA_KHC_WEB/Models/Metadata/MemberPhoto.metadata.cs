using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using System.Linq;
using System.Web;

namespace GEJA_KHC_WEB.Models
{
    [MetadataType(typeof(MemberPhotoMetadata))]
    public partial class MemberPhoto
    {
    }

    public class MemberPhotoMetadata
    {
        [Key, Required]
        [DisplayName("የአባል መለያ ቁጥር")]
        public string MemberId { get; set; }

        [DisplayName("የአባል ፎቶ")]
        public string PhotoFilePath { get; set; }

        [DisplayName("የአባል ፎቶ")]
        public byte[] Photo { get; set; }

        [DisplayName("ማብራሪያ")]
        public string Remark { get; set; }
    }
}