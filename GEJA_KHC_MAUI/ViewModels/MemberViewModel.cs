using System.Collections.ObjectModel;
using GEJA_KHC_MAUI.Models;

namespace GEJA_KHC_MAUI.ViewModels
{
    public class MemberViewModel : GKHCBaseViewModel
    {
        private Member _Member = new Member();
        private Group _Group = new Group();
        private Gender _Gender = new Gender();
        private Month _BirthMonth = new Month();
        private Month _MembershipMonth = new Month();
        private MembershipMeans _MembershipMeans = new MembershipMeans();
        
        private ObservableCollection<Group> _GroupList = new ObservableCollection<Group>(DbViewModel.GetGroupList().Result.OrderBy(m => m.GroupId));
        private ObservableCollection<Gender> _GenderList = new ObservableCollection<Gender>(DbViewModel.GetGenderList().Result.OrderBy(m => m.GenderId));
        private ObservableCollection<int> _BirthDateList = GetRangeOfValues(1, 30); // Date values of Ethiopian month (1 - 30)
        private ObservableCollection<Month> _BirthMonthList = new ObservableCollection<Month>(DbViewModel.GetMonthList().Result.OrderBy(m => m.MonthId));
        private ObservableCollection<int> _BirthYearList = GetRangeOfValues(1900, DateTime.Now.Year - 7); // Year values of (1900 - current year)
        private ObservableCollection<int> _MembershipDateList = GetRangeOfValues(1, 30); // Date values of Ethiopian month (1 - 30)
        private ObservableCollection<Month> _MembershipMonthList = new ObservableCollection<Month>(DbViewModel.GetMonthList().Result.OrderBy(m => m.MonthId));
        private ObservableCollection<int> _MembershipYearList = GetRangeOfValues(1900, DateTime.Now.Year - 7); // Year values of (1900 - current year)
        private ObservableCollection<MembershipMeans> _MembershipMeansList = new ObservableCollection<MembershipMeans>(DbViewModel.GetMembershipMeansList().Result.OrderBy(m => m.MembershipMeansId));

        private int _GroupIndex = -1;
        private int _GenderIndex = -1;
        private int _BirthDateIndex = -1;
        private int _BirthMonthIndex = -1;
        private int _BirthYearIndex = -1;
        private int _MembershipDateIndex = -1;
        private int _MembershipMonthIndex = -1;
        private int _MembershipYearIndex = -1;
        private int _MembershipMeansIndex = -1;

        private Command _SaveCommand;
        private bool _SaveSuccess;

        public MemberViewModel(Member member)
        {
            if (string.IsNullOrEmpty(member.MemberId))
                return;

            _Member = DbViewModel.GetMember(member.MemberId).Result;
            _Group = DbViewModel.GetGroup((int) member.GroupId).Result;
            _Gender = (member.GenderId != null) ? DbViewModel.GetGender((int)member.GenderId).Result : null;
            _BirthMonth = (member.BirthMonthId != null) ? DbViewModel.GetMonth((int)member.BirthMonthId).Result : null;
            _MembershipMonth = (member.MembershipMonthId != null) ? DbViewModel.GetMonth((int) member.MembershipMonthId).Result : null;
            _MembershipMeans = (member.MembershipMeansId != null) ? DbViewModel.GetMembershipMeans((int) member.MembershipMeansId).Result : null;

            // Loop through the pickers to find the index of the current item

            int i = 0;

            if (member.GroupId != null)
            {
                for (i = 0; i < _GroupList.Count; i++)
                    if (_GroupList[i].ToString() == _Group.ToString())
                        break;
                _GroupIndex = i;
            }

            if (member.GenderId != null)
            {
                for (i = 0; i < _GenderList.Count; i++)
                    if (_GenderList[i].ToString() == _Gender.ToString())
                        break;
                _GenderIndex = i;
            }

            if (member.BirthDate != null)
            {
                for (i = 0; i < _BirthDateList.Count; i++)
                    if (_BirthDateList[i] == member.BirthDate)
                        break;
                _BirthDateIndex = i;
            }

            if (member.BirthMonthId != null)
            {
                for (i = 0; i < _BirthMonthList.Count; i++)
                    if (_BirthMonthList[i].ToString() == _BirthMonth.ToString())
                        break;
                _BirthMonthIndex = i;
            }

            if (member.BirthYear != null)
            {
                for (i = 0; i < _BirthYearList.Count; i++)
                    if (_BirthYearList[i] == member.BirthYear)
                        break;
                _BirthYearIndex = i;
            }

            if (member.MembershipDate != null)
            {
                for (i = 0; i < _MembershipDateList.Count; i++)
                    if (_MembershipDateList[i] == member.MembershipDate)
                        break;
                _MembershipDateIndex = i;
            }

            if (member.MembershipMonthId != null)
            {
                for (i = 0; i < _MembershipMonthList.Count; i++)
                    if (_MembershipMonthList[i].ToString() == _MembershipMonth.ToString())
                        break;
                _MembershipMonthIndex = i;
            }

            if (member.MembershipYear != null)
            {
                for (i = 0; i < _MembershipYearList.Count; i++)
                    if (_MembershipYearList[i] == member.MembershipYear)
                        break;
                _MembershipYearIndex = i;
            }

            if (member.MembershipMeansId != null)
            {
                for (i = 0; i < _MembershipMeansList.Count; i++)
                    if (_MembershipMeansList[i].ToString() == _MembershipMeans.ToString())
                        break;
                _MembershipMeansIndex = i;
            }
        }

        public Member Member
        {
            get { return _Member; }
            set
            {
                _Member = value;
                OnPropertyChanged(nameof(Member));
            }
        }

        public string MemberId
        {
            get { return _Member.MemberId; }
            set
            {
                _Member.MemberId = value;
                OnPropertyChanged(nameof(MemberId));
            }
        }

        public string FullName
        {
            get { return _Member.FullName; }
            set
            {
                _Member.FullName = value;
                OnPropertyChanged(nameof(FullName));
            }
        }

        public string MotherName
        {
            get { return _Member.MotherName; }
            set
            {
                _Member.MotherName = value;
                OnPropertyChanged(nameof(MotherName));
            }
        }

        public ObservableCollection<Gender> GenderList
        {
            get
            {
                return new ObservableCollection<Gender>(DbViewModel.GetGenderList().Result.OrderBy(s => s.GenderId));
            }
            set
            {
                _GenderList = value;
                OnPropertyChanged(nameof(GenderList));
            }
        }

        public Gender Gender
        {
            get { return _Gender; }
            set
            {
                _Member.GenderId = (value != null) ? value.GenderId : null;
                _Gender = value;
                OnPropertyChanged(nameof(Gender));
            }
        }

        public int GenderIndex
        {
            get { return _GenderIndex; }
            set
            {
                // When "GenderIndex" is updated, also update "Gender"
                _GenderIndex = value;
                Gender = (value != -1) ? _GenderList[value] : null; ;
                OnPropertyChanged(nameof(GenderIndex));
            }
        }

        public ObservableCollection<int> BirthDateList
        {
            get { return _BirthDateList; }
        }

        public int? BirthDate
        {
            get { return _Member.BirthDate; }
            set
            {
                _Member.BirthDate = value;
                OnPropertyChanged(nameof(BirthDate));
            }
        }

        public int BirthDateIndex
        {
            get { return _BirthDateIndex; }
            set
            {
                // When "BirthDateIndex" is updated, also update "BirthDate"
                _BirthDateIndex = value;
                BirthDate = (value != -1) ? _BirthDateList[value] : null; ;
                OnPropertyChanged(nameof(BirthDateIndex));
            }
        }


        public ObservableCollection<Month> BirthMonthList
        {
            get
            {
                return new ObservableCollection<Month>(DbViewModel.GetMonthList().Result.OrderBy(s => s.MonthId));
            }
            set
            {
                _BirthMonthList = value;
                OnPropertyChanged(nameof(BirthMonthList));
            }
        }

        public Month BirthMonth
        {
            get { return _BirthMonth; }
            set
            {
                _Member.BirthMonthId = (value != null) ? value.MonthId : null;
                _BirthMonth = value;
                OnPropertyChanged(nameof(BirthMonth));
            }
        }

        public int BirthMonthIndex
        {
            get { return _BirthMonthIndex; }
            set
            {
                // When "BirthMonthIndex" is updated, also update "BirthMonth"
                _BirthMonthIndex = value;
                BirthMonth = (value != -1) ? _BirthMonthList[value] : null; ;
                OnPropertyChanged(nameof(BirthMonthIndex));
            }
        }

        public ObservableCollection<int> BirthYearList
        {
            get { return _BirthYearList; }
        }

        public int? BirthYear
        {
            get { return _Member.BirthYear; }
            set
            {
                _Member.BirthYear = value;
                OnPropertyChanged(nameof(BirthYear));
            }
        }

        public int BirthYearIndex
        {
            get { return _BirthYearIndex; }
            set
            {
                // When "BirthYearIndex" is updated, also update "BirthYear"
                _BirthYearIndex = value;
                BirthYear = (value != -1) ? _BirthYearList[value] : null; ;
                OnPropertyChanged(nameof(BirthYearIndex));
            }
        }

        public ObservableCollection<Group> GroupList
        {
            get
            {
                return new ObservableCollection<Group>(DbViewModel.GetGroupList().Result.OrderBy(s => s.GroupId));
            }
            set
            {
                _GroupList = value;
                OnPropertyChanged(nameof(GroupList));
            }
        }

        public Group Group
        {
            get { return _Group; }
            set
            {
                _Member.GroupId = (value != null) ? value.GroupId : null;
                _Group = value;
                OnPropertyChanged(nameof(Group));
            }
        }

        public int GroupIndex
        {
            get { return _GroupIndex; }
            set
            {
                // When "GroupIndex" is updated, also update "Group"
                _GroupIndex = value;
                Group = (value != -1) ? _GroupList[value] : null; ;
                OnPropertyChanged(nameof(GroupIndex));
            }
        }

        public ObservableCollection<int> MembershipDateList
        {
            get { return _MembershipDateList; }
        }

        public int? MembershipDate
        {
            get { return _Member.MembershipDate; }
            set
            {
                _Member.MembershipDate = value;
                OnPropertyChanged(nameof(MembershipDate));
            }
        }

        public int MembershipDateIndex
        {
            get { return _MembershipDateIndex; }
            set
            {
                // When "MembershipDateIndex" is updated, also update "MembershipDate"
                _MembershipDateIndex = value;
                MembershipDate = (value != -1) ? _MembershipDateList[value] : null; ;
                OnPropertyChanged(nameof(MembershipDateIndex));
            }
        }

        public ObservableCollection<Month> MembershipMonthList
        {
            get
            {
                return new ObservableCollection<Month>(DbViewModel.GetMonthList().Result.OrderBy(s => s.MonthId));
            }
            set
            {
                _MembershipMonthList = value;
                OnPropertyChanged(nameof(MembershipMonthList));
            }
        }

        public Month MembershipMonth
        {
            get { return _MembershipMonth; }
            set
            {
                _Member.MembershipMonthId = (value != null) ? value.MonthId : null;
                _MembershipMonth = value;
                OnPropertyChanged(nameof(MembershipMonth));
            }
        }

        public int MembershipMonthIndex
        {
            get { return _MembershipMonthIndex; }
            set
            {
                // When "MembershipMonthIndex" is updated, also update "MembershipMonth"
                _MembershipMonthIndex = value;
                MembershipMonth = (value != -1) ? _MembershipMonthList[value] : null; ;
                OnPropertyChanged(nameof(MembershipMonthIndex));
            }
        }

        public ObservableCollection<int> MembershipYearList
        {
            get { return _MembershipYearList; }
        }

        public int? MembershipYear
        {
            get { return _Member.MembershipYear; }
            set
            {
                _Member.MembershipYear = value;
                OnPropertyChanged(nameof(MembershipYear));
            }
        }

        public int MembershipYearIndex
        {
            get { return _MembershipYearIndex; }
            set
            {
                // When "MembershipYearIndex" is updated, also update "MembershipYear"
                _MembershipYearIndex = value;
                MembershipYear = (value != -1) ? _MembershipYearList[value] : null; ;
                OnPropertyChanged(nameof(MembershipYearIndex));
            }
        }

        public ObservableCollection<MembershipMeans> MembershipMeansList
        {
            get
            {
                return new ObservableCollection<MembershipMeans>(DbViewModel.GetMembershipMeansList().Result.OrderBy(s => s.MembershipMeansId));
            }
            set
            {
                _MembershipMeansList = value;
                OnPropertyChanged(nameof(MembershipMeansList));
            }
        }

        public MembershipMeans MembershipMeans
        {
            get { return _MembershipMeans; }
            set
            {
                _Member.MembershipMeansId = (value != null) ? value.MembershipMeansId : null;
                _MembershipMeans = value;
                OnPropertyChanged(nameof(MembershipMeans));
            }
        }

        public int MembershipMeansIndex
        {
            get { return _MembershipMeansIndex; }
            set
            {
                // When "MembershipMeansIndex" is updated, also update "MembershipMeans"
                _MembershipMeansIndex = value;
                MembershipMeans = (value != -1) ? _MembershipMeansList[value] : null; ;
                OnPropertyChanged(nameof(MembershipMeansIndex));
            }
        }
        
        public string NoServiceReason
        {
            get { return _Member.NoServiceReason; }
            set
            {
                _Member.NoServiceReason = value;
                OnPropertyChanged(nameof(NoServiceReason));
            }
        }

        public string Remark
        {
            get { return _Member.Remark; }
            set
            {
                _Member.Remark = value;
                OnPropertyChanged(nameof(Remark));
            }
        }

        public ImageSource PhotoStream
        {
            get
            {
                MemberPhoto photo = DbViewModel.GetMemberPhoto(_Member.MemberId).Result;
                var stream = new MemoryStream();
                if (photo.Photo != null)
                    stream = new MemoryStream(photo.Photo);

                return ImageSource.FromStream(() => stream);
            }
        }

        public string PhotoUrl
        {
            get 
            {
                MemberPhoto photo = DbViewModel.GetMemberPhoto(_Member.MemberId).Result;
                if (photo.PhotoFilePath != null)
                { 
                    if (photo.PhotoFilePath.Trim() != "")
                        return DbViewModel.GetWebApplicationUrl() + photo.PhotoFilePath;
                }

                return "no_photo";
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
                return _SaveCommand ?? (_SaveCommand = new Command(async () => await ExecuteOnMemberSave()));
            }
        }

        private async Task ExecuteOnMemberSave()
        {
            try
            {
                // If at least one required field is not entered or selected, return false. Otherwise, return true
                _SaveSuccess = !(string.IsNullOrEmpty(_Member.MemberId.Trim())) && !(string.IsNullOrEmpty(_Member.FullName.Trim())) 
                                            && (_Member.GroupId != null);
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
