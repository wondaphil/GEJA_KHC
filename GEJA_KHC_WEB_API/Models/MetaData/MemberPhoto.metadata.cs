using GEJA_KHC_WEB_API.Models.MetaData;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace GEJA_KHC_WEB_API.Models.MetaData
{
    [MetadataType(typeof(MemberPhotoMetadata))]
    public partial class MemberPhoto
    {
    }

    public partial class MemberPhotoMetadata
    {
        [Key, Required]
        [DisplayName("የአባል መለያ ቁጥር")]
        public string MemberId { get; set; }

        [DisplayName("የአባል ፎቶ")]
        public string PhotoFilePath { get; set; }

        [DisplayName("ፎቶ")]
        public byte[] Photo { get; set; }
        
        [DisplayName("ማብራሪያ")]
        public string Remark { get; set; }

        
        [JsonIgnore]
        public virtual Member Member { get; set; }
    }
}
