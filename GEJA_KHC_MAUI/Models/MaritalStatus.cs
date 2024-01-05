namespace GEJA_KHC_MAUI.Models
{
    public partial class MaritalStatus
    {
        public int MaritalStatusId { get; set; }
        public string MaritalStatusName { get; set; }
        public string Remark { get; set; }

        public override string ToString()
        {
            return $"{MaritalStatusName}";
        }
    }
}
