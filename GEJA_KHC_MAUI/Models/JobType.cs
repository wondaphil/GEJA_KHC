namespace GEJA_KHC_MAUI.Models
{
    public partial class JobType
    {
        public int JobTypeId { get; set; }
        public string JobTypeName { get; set; }
        public string Remark { get; set; }

        public override string ToString()
        {
            return $"{JobTypeName}";
        }
    }
}
