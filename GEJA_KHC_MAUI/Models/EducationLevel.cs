namespace GEJA_KHC_MAUI.Models
{
    public partial class EducationLevel
    {
        public int EducationLevelId { get; set; }
        public string EducationLevelName { get; set; }
        public string Remark { get; set; }

        public override string ToString()
        {
            return $"{EducationLevelName}";
        }
    }
}
