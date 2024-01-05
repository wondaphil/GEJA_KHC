using System.Collections.ObjectModel;
using GEJA_KHC_MAUI.Models;

namespace GEJA_KHC_MAUI.ViewModels
{
    public class MemberServiceViewModel : GKHCBaseViewModel
    {
        
        private Member _Member = new Member();
        private MemberService _MemberService = new MemberService();
        
        private ServiceType _ServiceType = new ServiceType();
        private ObservableCollection<ServiceType> _ServiceTypeList = new ObservableCollection<ServiceType>(DbViewModel.GetServiceTypeList().Result);
        private int _ServiceTypeIndex = -1; // set to unselected by default

        private Service _Service = new Service();
        private ObservableCollection<Service> _ServiceList = new ObservableCollection<Service>(DbViewModel.GetServiceList().Result);
        private int _ServiceIndex = -1; // set to unselected by default

        private Command _SaveCommand;
        private bool _SaveSuccess;

        public MemberServiceViewModel(MemberService memberService)
        {
            _MemberService = DbViewModel.GetMemberServiceById(memberService.Id).Result;
            _Member = DbViewModel.GetMember(memberService.MemberId).Result;

            //if (memberService.Id == null)
            if (_MemberService == null)
            {
                // Create new MemberService object with the given member id
                _MemberService = new MemberService() { MemberId = memberService.MemberId };

                return;
            }
            
            int i;
            _ServiceType = DbViewModel.GetServiceType((int) _MemberService.ServiceTypeId).Result;
            for (i = 0; i < _ServiceTypeList.Count; i++)
                if (_ServiceTypeList[i].ToString() == _ServiceType.ToString())
                    break;
            _ServiceTypeIndex = i;

            _Service = DbViewModel.GetService(_MemberService.ServiceId).Result;
            for (i = 0; i < _ServiceList.Count; i++)
                if (_ServiceList[i].ToString() == _Service.ToString())
                    break;
            _ServiceIndex = i;
        }

        public MemberService MemberService
        {
            get { return _MemberService; }
            set
            {
                _MemberService = value;
                OnPropertyChanged(nameof(MemberService));
            }
        }

        public int Id
        {
            get { return _MemberService.Id; }
            set
            {
                _MemberService.Id = value;
                OnPropertyChanged(nameof(Id));
            }
        }

        public string MemberId
        {
            get { return _Member.MemberId; }
            set
            {
                if (_MemberService != null)
                    _MemberService.MemberId = value;
                OnPropertyChanged("MemberId");
            }
        }

        public ObservableCollection<ServiceType> ServiceTypeList
        {
            get { return new ObservableCollection<ServiceType>(DbViewModel.GetServiceTypeList().Result); }
            set
            {
                _ServiceTypeList = value;
                OnPropertyChanged(nameof(ServiceTypeList));
            }
        }

        public ServiceType ServiceType
        {
            get { return _ServiceType; }
            set
            {
                if (_MemberService != null)
                    _MemberService.ServiceTypeId = value.ServiceTypeId;
                _ServiceType = value;
                OnPropertyChanged(nameof(ServiceType));
            }
        }

        public int ServiceTypeIndex
        {
            get { return _ServiceTypeIndex; }
            set
            {
                // When "ServiceTypeIndex" is updated, also update "ServiceType"
                _ServiceTypeIndex = value;
                ServiceType = (value != -1) ? _ServiceTypeList[value] : null;

                OnPropertyChanged(nameof(ServiceTypeIndex));
            }
        }

        public ObservableCollection<Service> ServiceList
        {
            get { return new ObservableCollection<Service>(DbViewModel.GetServiceList().Result); }
            set
            {
                _ServiceList = value;
                OnPropertyChanged(nameof(ServiceList));
            }
        }

        public Service Service
        {
            get { return _Service; }
            set
            {
                if (_MemberService != null)
                    _MemberService.ServiceId = value.ServiceId;
                _Service = value;
                OnPropertyChanged(nameof(Service));
            }
        }

        public int ServiceIndex
        {
            get { return _ServiceIndex; }
            set
            {
                // When "ServiceIndex" is updated, also update "Service"
                _ServiceIndex = value;
                Service = (value != -1) ? _ServiceList[value] : null;

                OnPropertyChanged(nameof(ServiceIndex));
            }
        }

        public bool SaveSuccess
        {
            get
            {
                return _SaveSuccess;
            }
        }

        public Command SaveCommand
        {
            get
            {
                return _SaveCommand ?? (_SaveCommand = new Command(async () => await ExecuteOnServiceSave()));
            }
        }

        private async Task ExecuteOnServiceSave()
        {
            try
            {
                // If at least one required field is not entered or selected, return false. Otherwise, return true
                _SaveSuccess = (ServiceTypeIndex != -1) && (ServiceIndex != -1);
            }
            catch (Exception)
            {
                await Task.Run(() => _SaveSuccess = false);
            }
        }
    }
}