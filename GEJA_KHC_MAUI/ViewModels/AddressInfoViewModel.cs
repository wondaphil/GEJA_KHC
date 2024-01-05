using System.Collections.ObjectModel;
using GEJA_KHC_MAUI.Models;

namespace GEJA_KHC_MAUI.ViewModels
{
    public class AddressInfoViewModel : GKHCBaseViewModel
    {
        private Member _Member = new Member();
        private AddressInfo _AddressInfo = new AddressInfo();
        
        private Subcity _Subcity = new Subcity();
        private HouseOwnershipType _HouseOwnershipType = new HouseOwnershipType();

        private ObservableCollection<Subcity> _SubcityList = new ObservableCollection<Subcity>(DbViewModel.GetSubcityList().Result.OrderBy(m => m.SubcityId));
        private ObservableCollection<HouseOwnershipType> _HouseOwnershipTypeList = new ObservableCollection<HouseOwnershipType>(DbViewModel.GetHouseOwnershipTypeList().Result.OrderBy(m => m.HouseOwnershipTypeId));

        private int _SubcityIndex = -1;
        private int _HouseOwnershipTypeIndex = -1;

        private Command _SaveCommand;
        private bool _SaveSuccess;

        public AddressInfoViewModel(Member member)
        {
            _Member = member;
            if (string.IsNullOrEmpty(_Member.MemberId))
                return;

            _AddressInfo = DbViewModel.GetAddressInfo(_Member.MemberId).Result;
            
            // Loop through the pickers to find the index of the current item
            int i = 0;

            _Subcity = (_AddressInfo.SubcityId != null) ? DbViewModel.GetSubcity((int)_AddressInfo.SubcityId).Result : null;
            if (_AddressInfo.SubcityId != null)
            {
                for (i = 0; i < _SubcityList.Count; i++)
                    if (_SubcityList[i].ToString() == _Subcity.ToString())
                        break;
                _SubcityIndex = i;
            }

            _HouseOwnershipType = (_AddressInfo.HouseOwnershipId != null) ? DbViewModel.GetHouseOwnershipType((int)_AddressInfo.HouseOwnershipId).Result : null;
            if (_AddressInfo.HouseOwnershipId != null)
            {
                for (i = 0; i < _HouseOwnershipTypeList.Count; i++)
                    if (_HouseOwnershipTypeList[i].ToString() == _HouseOwnershipType.ToString())
                        break;
                _HouseOwnershipTypeIndex = i;
            }
        }

        public AddressInfo AddressInfo
        {
            get { return _AddressInfo; }
            set
            {
                _AddressInfo = value;
                OnPropertyChanged(nameof(AddressInfo));
            }
        }

        public string MemberId
        {
            get { return _AddressInfo.MemberId; }
            set
            {
                _AddressInfo.MemberId = value;
                OnPropertyChanged(nameof(MemberId));
            }
        }

        public ObservableCollection<Subcity> SubcityList
        {
            get
            {
                return new ObservableCollection<Subcity>(DbViewModel.GetSubcityList().Result.OrderBy(s => s.SubcityId));
            }
            set
            {
                _SubcityList = value;
                OnPropertyChanged(nameof(SubcityList));
            }
        }

        public Subcity Subcity
        {
            get { return _Subcity; }
            set
            {
                _AddressInfo.SubcityId = (value != null) ? value.SubcityId : null;
                _Subcity = value;
                OnPropertyChanged(nameof(Subcity));
            }
        }

        public int SubcityIndex
        {
            get { return _SubcityIndex; }
            set
            {
                // When "SubcityIndex" is updated, also update "Subcity"
                _SubcityIndex = value;
                Subcity = (value != -1) ? _SubcityList[value] : null; ;
                OnPropertyChanged(nameof(SubcityIndex));
            }
        }

        public string Woreda
        {
            get { return _AddressInfo.Woreda; }
            set
            {
                _AddressInfo.Woreda = value;
                OnPropertyChanged(nameof(Woreda));
            }
        }

        public string Kebele
        {
            get { return _AddressInfo.Kebele; }
            set
            {
                _AddressInfo.Kebele = value;
                OnPropertyChanged(nameof(Kebele));
            }
        }

        public string HouseNo
        {
            get { return _AddressInfo.HouseNo; }
            set
            {
                _AddressInfo.HouseNo = value;
                OnPropertyChanged(nameof(HouseNo));
            }
        }

        public ObservableCollection<HouseOwnershipType> HouseOwnershipTypeList
        {
            get
            {
                return new ObservableCollection<HouseOwnershipType>(DbViewModel.GetHouseOwnershipTypeList().Result.OrderBy(s => s.HouseOwnershipTypeId));
            }
            set
            {
                _HouseOwnershipTypeList = value;
                OnPropertyChanged(nameof(HouseOwnershipTypeList));
            }
        }

        public HouseOwnershipType HouseOwnershipType
        {
            get { return _HouseOwnershipType; }
            set
            {
                _AddressInfo.HouseOwnershipId = (value != null) ? value.HouseOwnershipTypeId : null;
                _HouseOwnershipType = value;
                OnPropertyChanged(nameof(HouseOwnershipType));
            }
        }

        public int HouseOwnershipTypeIndex
        {
            get { return _HouseOwnershipTypeIndex; }
            set
            {
                // When "HouseOwnershipTypeIndex" is updated, also update "HouseOwnershipType"
                _HouseOwnershipTypeIndex = value;
                HouseOwnershipType = (value != -1) ? _HouseOwnershipTypeList[value] : null; ;
                OnPropertyChanged(nameof(HouseOwnershipTypeIndex));
            }
        }

        public string HomePhoneNo
        {
            get { return _AddressInfo.HomePhoneNo; }
            set
            {
                _AddressInfo.HomePhoneNo = value;
                OnPropertyChanged(nameof(HomePhoneNo));
            }
        }

        public string OfficePhoneNo
        {
            get { return _AddressInfo.OfficePhoneNo; }
            set
            {
                _AddressInfo.OfficePhoneNo = value;
                OnPropertyChanged(nameof(OfficePhoneNo));
            }
        }

        public string MobilePhoneNo
        {
            get { return _AddressInfo.MobilePhoneNo; }
            set
            {
                _AddressInfo.MobilePhoneNo = value;
                OnPropertyChanged(nameof(MobilePhoneNo));
            }
        }

        public string Email
        {
            get { return _AddressInfo.Email; }
            set
            {
                _AddressInfo.Email = value;
                OnPropertyChanged(nameof(Email));
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
                return _SaveCommand ?? (_SaveCommand = new Command(async () => await ExecuteOnAddressInfoSave()));
            }
        }

        private async Task ExecuteOnAddressInfoSave()
        {
            try
            {
                // If at least one required field is not entered or selected, return false. Otherwise, return true
                _SaveSuccess = (_AddressInfo.SubcityId != null);
            }
            catch (Exception)
            {
                await Task.Run(() => _SaveSuccess = false);
            }
        }
    }
}
