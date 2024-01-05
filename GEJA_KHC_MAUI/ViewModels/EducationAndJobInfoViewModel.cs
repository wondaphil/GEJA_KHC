using System.Collections.ObjectModel;
using GEJA_KHC_MAUI.Models;

namespace GEJA_KHC_MAUI.ViewModels
{
    public class EducationAndJobInfoViewModel : GKHCBaseViewModel
    {
        private Member _Member = new Member();
        private EducationAndJobInfo _EducationAndJobInfo = new EducationAndJobInfo();

        private EducationLevel _EducationLevel = new EducationLevel();
        private FieldOfStudy _FieldOfStudy = new FieldOfStudy();
        private Job _Job = new Job();
        private JobType _JobType = new JobType();
        
        private ObservableCollection<EducationLevel> _EducationLevelList = new ObservableCollection<EducationLevel>(DbViewModel.GetEducationLevelList().Result.OrderBy(m => m.EducationLevelId));
        private ObservableCollection<FieldOfStudy> _FieldOfStudyList = new ObservableCollection<FieldOfStudy>(DbViewModel.GetFieldOfStudyList().Result.OrderBy(m => m.FieldOfStudyId));
        private ObservableCollection<Job> _JobList = new ObservableCollection<Job>(DbViewModel.GetJobList().Result.OrderBy(m => m.JobId));
        private ObservableCollection<JobType> _JobTypeList = new ObservableCollection<JobType>(DbViewModel.GetJobTypeList().Result.OrderBy(m => m.JobTypeId));
        
        private int _EducationLevelIndex = -1;
        private int _FieldOfStudyIndex = -1;
        private int _JobIndex = -1;
        private int _JobTypeIndex = -1;

        private Command _SaveCommand;
        private bool _SaveSuccess;

        public EducationAndJobInfoViewModel(Member member)
        {
            _Member = member;
            if (string.IsNullOrEmpty(_Member.MemberId))
                return;

            _EducationAndJobInfo = DbViewModel.GetEducationAndJobInfo(_Member.MemberId).Result;

            // Loop through the pickers to find the index of the current item

            int i = 0;

            _EducationLevel = (_EducationAndJobInfo.EducationLevelId != -1) ? DbViewModel.GetEducationLevel((int)_EducationAndJobInfo.EducationLevelId).Result : null;
            if (_EducationAndJobInfo.EducationLevelId != -1)
            {
                for (i = 0; i < _EducationLevelList.Count; i++)
                    if (_EducationLevelList[i].ToString() == _EducationLevel.ToString())
                        break;
                _EducationLevelIndex = i;
            }

            _FieldOfStudy = (_EducationAndJobInfo.FieldOfStudyId != -1) ? DbViewModel.GetFieldOfStudy((int)_EducationAndJobInfo.FieldOfStudyId).Result : null;
            if (_EducationAndJobInfo.FieldOfStudyId != -1)
            {
                for (i = 0; i < _FieldOfStudyList.Count; i++)
                    if (_FieldOfStudyList[i].ToString() == _FieldOfStudy.ToString())
                        break;
                _FieldOfStudyIndex = i;
            }

            _Job = (_EducationAndJobInfo.JobId != -1) ? DbViewModel.GetJob((int)_EducationAndJobInfo.JobId).Result : null;
            if (_EducationAndJobInfo.JobId != -1)
            {
                for (i = 0; i < _JobList.Count; i++)
                    if (_JobList[i].ToString() == _Job.ToString())
                        break;
                _JobIndex = i;
            }

            _JobType = (_EducationAndJobInfo.JobTypeId != -1) ? DbViewModel.GetJobType((int)_EducationAndJobInfo.JobTypeId).Result : null;
            if (_EducationAndJobInfo.JobTypeId != -1)
            {
                for (i = 0; i < _JobTypeList.Count; i++)
                    if (_JobTypeList[i].ToString() == _JobType.ToString())
                        break;
                _JobTypeIndex = i;
            }
        }

        public EducationAndJobInfo EducationAndJobInfo
        {
            get { return _EducationAndJobInfo; }
            set
            {
                _EducationAndJobInfo = value;
                OnPropertyChanged(nameof(EducationAndJobInfo));
            }
        }

        public string MemberId
        {
            get { return _EducationAndJobInfo.MemberId; }
            set
            {
                _EducationAndJobInfo.MemberId = value;
                OnPropertyChanged(nameof(MemberId));
            }
        }

        public ObservableCollection<EducationLevel> EducationLevelList
        {
            get
            {
                return new ObservableCollection<EducationLevel>(DbViewModel.GetEducationLevelList().Result.OrderBy(s => s.EducationLevelId));
            }
            set
            {
                _EducationLevelList = value;
                OnPropertyChanged(nameof(EducationLevelList));
            }
        }

        public EducationLevel EducationLevel
        {
            get { return _EducationLevel; }
            set
            {
                _EducationAndJobInfo.EducationLevelId = (value != null) ? value.EducationLevelId : -1;
                _EducationLevel = value;
                OnPropertyChanged(nameof(EducationLevel));
            }
        }

        public int EducationLevelIndex
        {
            get { return _EducationLevelIndex; }
            set
            {
                // When "EducationLevelIndex" is updated, also update "EducationLevel"
                _EducationLevelIndex = value;
                EducationLevel = (value != -1) ? _EducationLevelList[value] : null; ;
                OnPropertyChanged(nameof(EducationLevelIndex));
            }
        }

        public ObservableCollection<FieldOfStudy> FieldOfStudyList
        {
            get
            {
                return new ObservableCollection<FieldOfStudy>(DbViewModel.GetFieldOfStudyList().Result.OrderBy(s => s.FieldOfStudyId));
            }
            set
            {
                _FieldOfStudyList = value;
                OnPropertyChanged(nameof(FieldOfStudyList));
            }
        }

        public FieldOfStudy FieldOfStudy
        {
            get { return _FieldOfStudy; }
            set
            {
                _EducationAndJobInfo.FieldOfStudyId = (value != null) ? value.FieldOfStudyId : -1;
                _FieldOfStudy = value;
                OnPropertyChanged(nameof(FieldOfStudy));
            }
        }

        public int FieldOfStudyIndex
        {
            get { return _FieldOfStudyIndex; }
            set
            {
                // When "FieldOfStudyIndex" is updated, also update "FieldOfStudy"
                _FieldOfStudyIndex = value;
                FieldOfStudy = (value != -1) ? _FieldOfStudyList[value] : null; ;
                OnPropertyChanged(nameof(FieldOfStudyIndex));
            }
        }

        public ObservableCollection<Job> JobList
        {
            get
            {
                return new ObservableCollection<Job>(DbViewModel.GetJobList().Result.OrderBy(s => s.JobId));
            }
            set
            {
                _JobList = value;
                OnPropertyChanged(nameof(JobList));
            }
        }

        public Job Job
        {
            get { return _Job; }
            set
            {
                _EducationAndJobInfo.JobId = (value != null) ? value.JobId : -1;
                _Job = value;
                OnPropertyChanged(nameof(Job));
            }
        }

        public int JobIndex
        {
            get { return _JobIndex; }
            set
            {
                // When "JobIndex" is updated, also update "Job"
                _JobIndex = value;
                Job = (value != -1) ? _JobList[value] : null; ;
                OnPropertyChanged(nameof(JobIndex));
            }
        }

        public ObservableCollection<JobType> JobTypeList
        {
            get
            {
                return new ObservableCollection<JobType>(DbViewModel.GetJobTypeList().Result.OrderBy(s => s.JobTypeId));
            }
            set
            {
                _JobTypeList = value;
                OnPropertyChanged(nameof(JobTypeList));
            }
        }

        public JobType JobType
        {
            get { return _JobType; }
            set
            {
                _EducationAndJobInfo.JobTypeId = (value != null) ? value.JobTypeId : -1;
                _JobType = value;
                OnPropertyChanged(nameof(JobType));
            }
        }

        public int JobTypeIndex
        {
            get { return _JobTypeIndex; }
            set
            {
                // When "JobTypeIndex" is updated, also update "JobType"
                _JobTypeIndex = value;
                JobType = (value != -1) ? _JobTypeList[value] : null; ;
                OnPropertyChanged(nameof(JobTypeIndex));
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
                return _SaveCommand ?? (_SaveCommand = new Command(async () => await ExecuteOnEducationAndJobInfoSave()));
            }
        }

        private async Task ExecuteOnEducationAndJobInfoSave()
        {
            try
            {
                // If at least one required field is not entered or selected, return false. Otherwise, return true
                _SaveSuccess = (_EducationAndJobInfo.EducationLevelId != -1) && (_EducationAndJobInfo.FieldOfStudyId != -1) && 
                        (_EducationAndJobInfo.JobId != -1) && (_EducationAndJobInfo.JobTypeId != -1);
            }
            catch (Exception)
            {
                await Task.Run(() => _SaveSuccess = false);
            }
        }
    }
}
