namespace GEJA_KHC_MAUI.Models
{
    public partial class FieldOfStudy
    {
        public int FieldOfStudyId { get; set; }
        public string FieldOfStudyName { get; set; }
        public string EnglishName { get; set; }
        public string Remark { get; set; }

        public override string ToString()
        {
            return $"{FieldOfStudyName}";
        }
    }
}
