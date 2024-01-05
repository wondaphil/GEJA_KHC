using System.ComponentModel.DataAnnotations;

namespace GEJA_KHC_MAUI.Models
{
    public class Group
    {
        [Key, Required]
        public int GroupId { get; set; }
        public string GroupName { get; set; }
        public string Pastor { get; set; }
        public string Remark { get; set; }

        public override string ToString()
        {
            return $"{GroupName}";
        }
    }
}