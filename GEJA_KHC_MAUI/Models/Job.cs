namespace GEJA_KHC_MAUI.Models
{
    public partial class Job
    {
        public int JobId { get; set; }
        public string JobName { get; set; }
        public string Remark { get; set; }

        public override string ToString()
        {
            return $"{JobName}";
        }
    }
}
