namespace GEJA_KHC_MAUI.Models
{
    public partial class MembershipMeans
    {
        public int MembershipMeansId { get; set; }
        public string MembershipMeansName { get; set; }
        public string Remark { get; set; }

        public override string ToString()
        {
            return $"{MembershipMeansName}";
        }
    }
}
