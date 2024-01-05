using System.Collections.ObjectModel;
using GEJA_KHC_MAUI.Models;

namespace GEJA_KHC_MAUI.ViewModels
{
    public class MemberServiceListViewModel : GKHCBaseViewModel
    {
        private Member _Member;
        private ObservableCollection<MemberServiceDetailsViewModel> _MemberServiceList;
        
        public MemberServiceListViewModel(Member member)
        {
            _Member = member;
            ObservableCollection<MemberService> memberservices = new ObservableCollection<MemberService>(DbViewModel.GetMemberServiceList(member.MemberId).Result);
            _MemberServiceList = new ObservableCollection<MemberServiceDetailsViewModel>();

            foreach (var memserv in memberservices)
            {
                _MemberServiceList.Add(new MemberServiceDetailsViewModel()
                {
                    Id = memserv.Id,
                    MemberId = memserv.MemberId,
                    ServiceTypeName = DbViewModel.GetServiceType((int) memserv.ServiceTypeId).Result.ServiceTypeName,
                    ServiceName = DbViewModel.GetService((int)memserv.ServiceId).Result.ServiceName,
                    
                });
            }
        }

        public string MemberId
        {
            get { return _Member.MemberId; }
        }

        public ObservableCollection<MemberServiceDetailsViewModel> MemberServiceList
        {
            get { return _MemberServiceList; }
            set
            {
                _MemberServiceList = value;
                OnPropertyChanged(nameof(MemberServiceList));
            }
        }
    }
}
