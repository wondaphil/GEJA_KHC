using GEJA_KHC_MAUI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GEJA_KHC_MAUI.ViewModels
{
    public class GroupViewModel : GKHCBaseViewModel
    {
        private Group _Group = new Group();
        private Command _SaveCommand;
        private bool _SaveSuccess;

        public GroupViewModel(Group group)
        {
            if (group.GroupId == 0)
                return;

            _Group = DbViewModel.GetGroupList().Result.Where(d => d.GroupId == group.GroupId).FirstOrDefault();
        }

        public Group Group
        {
            get { return _Group; }
            set
            {
                _Group = value;
                OnPropertyChanged(nameof(Group));
            }
        }

        public string GroupId
        {
            get { return _Group.GroupId.ToString(); }
            set
            {
                _Group.GroupId = Convert.ToInt32(value);
                OnPropertyChanged(nameof(GroupId));
            }
        }

        public string GroupName
        {
            get { return _Group.GroupName; }
            set
            {
                _Group.GroupName = value;
                OnPropertyChanged(nameof(GroupName));
            }
        }

        public string Pastor
        {
            get { return _Group.Pastor; }
            set
            {
                _Group.Pastor = value;
                OnPropertyChanged(nameof(Pastor));
            }
        }

        public string Remark
        {
            get { return _Group.Remark; }
            set
            {
                _Group.Remark = value;
                OnPropertyChanged(nameof(Pastor));
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
                return _SaveCommand ?? (_SaveCommand = new Command(async () => await ExecuteOnGroupSave()));
            }
        }

        private async Task ExecuteOnGroupSave()
        {
            try
            {
                // If at least one required field is not entered or selected, return false. Otherwise, return true
                _SaveSuccess = !(string.IsNullOrEmpty(_Group.GroupId.ToString().Trim())) && !(string.IsNullOrEmpty(_Group.GroupName.Trim()));
            }
            catch (Exception)
            {
                await Task.Run(() => _SaveSuccess = false);
            }
        }
    }
}
