using System.Collections.ObjectModel;
using GEJA_KHC_MAUI.Models;

namespace GEJA_KHC_MAUI.ViewModels
{
    public class FamilyInfoViewModel : GKHCBaseViewModel
    {
        private Member _Member = new Member();
        private FamilyInfo _FamilyInfo = new FamilyInfo();

        private MaritalStatus _MaritalStatus = new MaritalStatus();
        
        private ObservableCollection<MaritalStatus> _MaritalStatusList = new ObservableCollection<MaritalStatus>(DbViewModel.GetMaritalStatusList().Result.OrderBy(m => m.MaritalStatusId));
        private ObservableCollection<int> _MarriageYearList = GetRangeOfValues(1900, DateTime.Now.Year - 7); // Year values of (1900 - current year)

        private int _MaritalStatusIndex = -1;
        private int _MarriageYearIndex = -1;

        private Command _SaveCommand;
        private bool _SaveSuccess;

        public FamilyInfoViewModel(Member member)
        {
            _Member = member;
            if (string.IsNullOrEmpty(_Member.MemberId))
                return;

            _FamilyInfo = DbViewModel.GetFamilyInfo(_Member.MemberId).Result;

            // Loop through the pickers to find the index of the current item
            int i = 0;

            _MaritalStatus = (_FamilyInfo.MaritalStatusId != -1) ? DbViewModel.GetMaritalStatus((int)_FamilyInfo.MaritalStatusId).Result : null;
            if (_FamilyInfo.MaritalStatusId != -1)
            {
                for (i = 0; i < _MaritalStatusList.Count; i++)
                    if (_MaritalStatusList[i].ToString() == _MaritalStatus.ToString())
                        break;
                _MaritalStatusIndex = i;
            }

            if (_FamilyInfo.MarriageYear != null)
            {
                for (i = 0; i < _MarriageYearList.Count; i++)
                    if (_MarriageYearList[i] == _FamilyInfo.MarriageYear)
                        break;
                _MarriageYearIndex = i;
            }
        }

        public FamilyInfo FamilyInfo
        {
            get { return _FamilyInfo; }
            set
            {
                _FamilyInfo = value;
                OnPropertyChanged(nameof(FamilyInfo));
            }
        }

        public string MemberId
        {
            get { return _FamilyInfo.MemberId; }
            set
            {
                _FamilyInfo.MemberId = value;
                OnPropertyChanged(nameof(MemberId));
            }
        }

        public ObservableCollection<MaritalStatus> MaritalStatusList
        {
            get
            {
                return new ObservableCollection<MaritalStatus>(DbViewModel.GetMaritalStatusList().Result.OrderBy(s => s.MaritalStatusId));
            }
            set
            {
                _MaritalStatusList = value;
                OnPropertyChanged(nameof(MaritalStatusList));
            }
        }

        public MaritalStatus MaritalStatus
        {
            get { return _MaritalStatus; }
            set
            {
                _FamilyInfo.MaritalStatusId = (value != null) ? value.MaritalStatusId : -1;
                _MaritalStatus = value;
                OnPropertyChanged(nameof(MaritalStatus));
            }
        }

        public int MaritalStatusIndex
        {
            get { return _MaritalStatusIndex; }
            set
            {
                // When "MaritalStatusIndex" is updated, also update "MaritalStatus"
                _MaritalStatusIndex = value;
                MaritalStatus = (value != -1) ? _MaritalStatusList[value] : null; ;
                OnPropertyChanged(nameof(MaritalStatusIndex));
            }
        }

        public string SpouseName
        {
            get { return _FamilyInfo.SpouseName; }
            set
            {
                _FamilyInfo.SpouseName = value;
                OnPropertyChanged(nameof(SpouseName));
            }
        }

        public ObservableCollection<int> MarriageYearList
        {
            get { return _MarriageYearList; }
        }

        public int? MarriageYear
        {
            get { return _FamilyInfo.MarriageYear; }
            set
            {
                _FamilyInfo.MarriageYear = value;
                OnPropertyChanged(nameof(MarriageYear));
            }
        }

        public int MarriageYearIndex
        {
            get { return _MarriageYearIndex; }
            set
            {
                // When "MarriageYearIndex" is updated, also update "MarriageYear"
                _MarriageYearIndex = value;
                MarriageYear = (value != -1) ? _MarriageYearList[value] : null; ;
                OnPropertyChanged(nameof(MarriageYearIndex));
            }
        }

        public int? NoOfSons
        {
            get { return _FamilyInfo.NoOfSons; }
            set
            {
                _FamilyInfo.NoOfSons = value;
                OnPropertyChanged(nameof(NoOfSons));
            }
        }

        public int? NoOfSons1to5
        {
            get { return _FamilyInfo.NoOfSons1to5; }
            set
            {
                _FamilyInfo.NoOfSons1to5 = value;
                OnPropertyChanged(nameof(NoOfSons1to5));
            }
        }

        public int? NoOfSons6to12
        {
            get { return _FamilyInfo.NoOfSons6to12; }
            set
            {
                _FamilyInfo.NoOfSons6to12 = value;
                OnPropertyChanged(nameof(NoOfSons6to12));
            }
        }

        public int? NoOfSons13to20
        {
            get { return _FamilyInfo.NoOfSons13to20; }
            set
            {
                _FamilyInfo.NoOfSons13to20 = value;
                OnPropertyChanged(nameof(NoOfSons13to20));
            }
        }

        public int? NoOfSonsAbove20
        {
            get { return _FamilyInfo.NoOfSonsAbove20; }
            set
            {
                _FamilyInfo.NoOfSonsAbove20 = value;
                OnPropertyChanged(nameof(NoOfSonsAbove20));
            }
        }

        public int? NoOfDaughters
        {
            get { return _FamilyInfo.NoOfDaughters; }
            set
            {
                _FamilyInfo.NoOfDaughters = value;
                OnPropertyChanged(nameof(NoOfDaughters));
            }
        }

        public int? NoOfDaughters1to5
        {
            get { return _FamilyInfo.NoOfDaughters1to5; }
            set
            {
                _FamilyInfo.NoOfDaughters1to5 = value;
                OnPropertyChanged(nameof(NoOfDaughters1to5));
            }
        }

        public int? NoOfDaughters6to12
        {
            get { return _FamilyInfo.NoOfDaughters6to12; }
            set
            {
                _FamilyInfo.NoOfDaughters6to12 = value;
                OnPropertyChanged(nameof(NoOfDaughters6to12));
            }
        }

        public int? NoOfDaughters13to20
        {
            get { return _FamilyInfo.NoOfDaughters13to20; }
            set
            {
                _FamilyInfo.NoOfDaughters13to20 = value;
                OnPropertyChanged(nameof(NoOfDaughters13to20));
            }
        }

        public int? NoOfDaughtersAbove20
        {
            get { return _FamilyInfo.NoOfDaughtersAbove20; }
            set
            {
                _FamilyInfo.NoOfDaughtersAbove20 = value;
                OnPropertyChanged(nameof(NoOfDaughtersAbove20));
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
                return _SaveCommand ?? (_SaveCommand = new Command(async () => await ExecuteOnFamilyInfoSave()));
            }
        }

        private async Task ExecuteOnFamilyInfoSave()
        {
            try
            {
                // If at least one required field is not entered or selected, return false. Otherwise, return true
                _SaveSuccess = (_FamilyInfo.MaritalStatusId != -1);
            }
            catch (Exception)
            {
                await Task.Run(() => _SaveSuccess = false);
            }
        }

        /// <summary>
        /// Get a list of int values within the range start and end
        /// </summary>
        /// <param name="start">The starting value of the range</param>
        /// <param name="end">The ending value of the range</param>
        /// <returns></returns>
        public static ObservableCollection<int> GetRangeOfValues(int start, int end)
        {
            ObservableCollection<int> numlist = new ObservableCollection<int>();
            for (int i = start; i <= end; i++)
                numlist.Add(i);

            return numlist;
        }
    }
}
